using Client.Components.Movement;
using Client.Components.Visual;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client.Systems {
    internal sealed class AnimatorMoveStateListenerSystem : IEcsRunSystem {
        private const string MOVE_STATE = "MoveState";

        private readonly EcsFilterInject<
            Inc<AnimatorView, NavMeshAgentComponent>> _filter;

        public void Run(IEcsSystems systems) {

            foreach (int entity in _filter.Value) {
                ref var animator = ref _filter.Pools.Inc1.Get(entity).Value;
                ref var navMeshAgent = ref _filter.Pools.Inc2.Get(entity).Agent;

                if (navMeshAgent != null && navMeshAgent.velocity.magnitude > 0)
                    animator.SetFloat(MOVE_STATE, 1);
                else
                    animator.SetFloat(MOVE_STATE, 0);
            }
        }
    }
}
