using Client.Components.Health;
using Client.Components.Visual;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems {

    internal sealed class AnimatorDeathListenerSystem : IEcsRunSystem {
        private static readonly int _deathAnimatorTrigger = Animator.StringToHash("Death");

        private readonly EcsFilterInject<Inc<AnimatorView, DeathRequest>> _filter;

        public void Run(IEcsSystems systems) {

            foreach (int entity in _filter.Value) {
                var animator = _filter.Pools.Inc1.Get(entity).Value;
                animator.SetTrigger(_deathAnimatorTrigger);

                //Debug.Log($"AnimatorDeathListenerSystem: {entity}");
                _filter.Pools.Inc1.Del(entity);
            }
        }
    }
}
