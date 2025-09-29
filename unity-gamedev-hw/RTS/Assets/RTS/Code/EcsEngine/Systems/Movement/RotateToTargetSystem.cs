using Client.Components.Attack;
using Client.Components.Common;
using Client.Components.Movement;
using Client.Components.Targeting;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems {

    public sealed class RotateToTargetSystem : IEcsRunSystem {
        private readonly EcsFilterInject<
            Inc<Position, Rotation, TargetingComponent, Stopped>,
            Exc<Inactive>> _filter;

        private readonly EcsPoolInject<RotationSpeed> _rotationSpeedPool;

        public void Run(IEcsSystems systems) {
            foreach (int entity in _filter.Value) {
                ref var position = ref _filter.Pools.Inc1.Get(entity);
                ref var rotation = ref _filter.Pools.Inc2.Get(entity);
                ref var targeting = ref _filter.Pools.Inc3.Get(entity);

                if (targeting.CurrentTargetId == -1)
                    continue;

                ref var targetPosition = ref _filter.Pools.Inc1.Get(targeting.CurrentTargetId);

                Vector3 toTarget = targetPosition.Value - position.Value;
                toTarget.y = 0;

                if (toTarget == Vector3.zero)
                    continue;

                // Вычисляем целевой поворот
                Quaternion targetRotation = Quaternion.LookRotation(toTarget);

                // Проверяем, не смотрит ли юнит уже в нужном направлении
                float angleToTarget = Quaternion.Angle(rotation.Value, targetRotation);

                // Если угол больше 90 градусов - разворачиваем
                if (angleToTarget > 90f) {
                    if (_rotationSpeedPool.Value.Has(entity)) {
                        float rotSpeed = _rotationSpeedPool.Value.Get(entity).Value;
                        rotation.Value = Quaternion.RotateTowards(
                            rotation.Value,
                            targetRotation,
                            rotSpeed * Time.deltaTime
                        );
                    } else {
                        rotation.Value = targetRotation;
                    }
                }

                rotation.Value = targetRotation;
            }
        }
    }
}

