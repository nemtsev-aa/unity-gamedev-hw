using Client.Components.Common;
using Client.Components.Health;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client.Systems {
    internal sealed class DeathRequestSystem : IEcsRunSystem {
        private readonly EcsFilterInject<
            Inc<DeathRequest, DestroyOneFrame>,
            Exc<Inactive>> _filter;

        private readonly EcsPoolInject<Inactive> _inactivePool;
        private readonly EcsPoolInject<DeathEvent> _eventPool;

        public void Run(IEcsSystems systems) {

            foreach (int entity in _filter.Value) {
                _filter.Pools.Inc1.Del(entity);

                _inactivePool.Value.Add(entity) = new Inactive();
                _eventPool.Value.Add(entity) = new DeathEvent();

                //Debug.Log($"DeathRequestSystem: {entity} add DeathEvent!");
            }
        }
    }
}
