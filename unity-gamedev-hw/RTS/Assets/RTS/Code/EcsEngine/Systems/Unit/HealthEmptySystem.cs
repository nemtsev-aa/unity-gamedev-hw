using Client.Components.Common;
using Client.Components.Health;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client.Systems {

    public sealed class HealthEmptySystem : IEcsRunSystem {
        private readonly EcsFilterInject<
            Inc<Health>,
            Exc<DeathRequest, Inactive>> _filter;

        private readonly EcsPoolInject<DeathRequest> _deathPool;
        private readonly EcsPoolInject<Inactive> _inactivePool;

        public void Run(IEcsSystems systems) {

            foreach (int entity in _filter.Value) {
                Health health = _filter.Pools.Inc1.Get(entity);

                if (health.Value <= 0) {
                    _deathPool.Value.Add(entity) = new DeathRequest();
                    _inactivePool.Value.Add(entity) = new Inactive();

                    //Debug.Log($"HealthEmptySystem: {entity} add DeathRequest!");
                }
            }
        }
    }
}
