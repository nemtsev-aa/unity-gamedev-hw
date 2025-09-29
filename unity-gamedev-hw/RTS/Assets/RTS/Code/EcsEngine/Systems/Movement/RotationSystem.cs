using Client.Components.Common;
using Client.Components.Movement;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems {
    public sealed class RotationSystem : IEcsRunSystem {
        private readonly EcsFilterInject<
            Inc<RotationTarget, RotationSpeed, Rotation>,
            Exc<Inactive>> _filter;

        public void Run(IEcsSystems systems) {
            float deltaTime = Time.deltaTime;

            var targetRotationPool = _filter.Pools.Inc1;
            var rotationSpeedPool = _filter.Pools.Inc2;
            var rotationPool = _filter.Pools.Inc3;

            foreach (int entity in _filter.Value) {
                RotationTarget targetRotation = targetRotationPool.Get(entity);
                RotationSpeed rotationSpeed = rotationSpeedPool.Get(entity);
                ref Rotation rotation = ref rotationPool.Get(entity);

                rotation.Value = Quaternion.RotateTowards(
                    rotation.Value,
                    targetRotation.Value,
                    rotationSpeed.Value * deltaTime
                );

                if (Quaternion.Angle(rotation.Value, targetRotation.Value) < 0.1f)
                    targetRotationPool.Del(entity);
            }
        }
    }
}

