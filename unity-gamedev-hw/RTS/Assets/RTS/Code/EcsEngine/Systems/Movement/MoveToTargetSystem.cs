using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Client.Components.Attack;
using Client.Components.Common;
using Client.Components.Movement;
using Client.Components.Targeting;
using Unit = Client.Components.Common.Unit;

namespace Client.Systems {

    public sealed class MoveToTargetSystem : IEcsRunSystem {
        private readonly EcsFilterInject<
            Inc<Position, Rotation, MoveDirection, AttackTarget, Unit, MoveSpeed, TargetingComponent>,
            Exc<Inactive, Attacking>> _filter;

        private readonly EcsPoolInject<RotationSpeed> _rotationSpeedPool;
        private readonly EcsPoolInject<Stopped> _stoppedPool;

        public void Run(IEcsSystems systems) {
            float deltaTime = Time.deltaTime;

            foreach (int entity in _filter.Value) {
                ref var position = ref _filter.Pools.Inc1.Get(entity);
                ref var rotation = ref _filter.Pools.Inc2.Get(entity);
                ref var moveDirection = ref _filter.Pools.Inc3.Get(entity);
                ref var attackTarget = ref _filter.Pools.Inc4.Get(entity);
                ref var moveSpeed = ref _filter.Pools.Inc6.Get(entity);
                ref var targeting = ref _filter.Pools.Inc7.Get(entity);

                if (targeting.CurrentTargetId == -1) {

                    if (_stoppedPool.Value.Has(entity) == false)
                        _stoppedPool.Value.Add(entity);

                    continue;
                }

                attackTarget.LastKnownPosition = targeting.LastKnownTargetPosition;

                // 1. Вычисляем направление к цели (горизонтальная плоскость)
                Vector3 toTarget = attackTarget.LastKnownPosition - position.Value;
                toTarget.y = 0;

                if (toTarget == Vector3.zero)
                    continue;

                // 2. Движение вперед по текущему направлению поворота
                moveDirection.Value = rotation.Value * Vector3.forward;
                float distance = toTarget.magnitude;

                if (distance > attackTarget.ModelRadius)
                    position.Value += moveDirection.Value * (moveSpeed.Value * deltaTime);

                //Debug.Log($"Entity {entity} moving. Distance: {(int)distance}");
            }
        }
    }
}

