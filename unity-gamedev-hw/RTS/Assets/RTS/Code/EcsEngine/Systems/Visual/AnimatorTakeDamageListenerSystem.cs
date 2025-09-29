using Client.Components;
using Client.Components.Common;
using Client.Components.Health;
using Client.Components.Visual;
using Client.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems {
    internal sealed class AnimatorTakeDamageListenerSystem : IEcsRunSystem {
        private static readonly int TakeDamageAnimatorTrigger = Animator.StringToHash("TakeDamage");

        private readonly EcsFilterInject<
            Inc<TakeDamageEvent, TargetEntity>,
            Exc<DeathRequest, Inactive>> _filter = EcsWorlds.EVENTS;

        private readonly EcsPoolInject<AnimatorView> _animatorPool;

        public void Run(IEcsSystems systems) {

            foreach (int @event in _filter.Value) {
                int target = _filter.Pools.Inc2.Get(@event).Value;

                if (_animatorPool.Value.Has(target)) {
                    var animator = _animatorPool.Value.Get(target).Value;
                    animator.SetTrigger(TakeDamageAnimatorTrigger);

                    //Debug.Log($"AnimatorTakeDamageListenerSystem: Run!");
                }
            }
        }
    }
}
