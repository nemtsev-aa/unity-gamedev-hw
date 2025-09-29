using Client.Components.Attack;
using Client.Components.Visual;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems {
    internal sealed class AnimatorAttackRequestListenerSystem : IEcsRunSystem {
        private static readonly int _attackAnimatorTrigger = Animator.StringToHash("Attack");

        private readonly EcsFilterInject<Inc<AnimatorView, AttackRequest>> _filter;

        public void Run(IEcsSystems systems) {
            EcsPool<AnimatorView> animatorViewPool = _filter.Pools.Inc1;
            EcsPool<AttackRequest> requestPool = _filter.Pools.Inc2;

            foreach (int entity in _filter.Value) {
                var animator = animatorViewPool.Get(entity).Value;

                animator.SetTrigger(_attackAnimatorTrigger);

                requestPool.Del(entity);
            }
        }
    }
}
