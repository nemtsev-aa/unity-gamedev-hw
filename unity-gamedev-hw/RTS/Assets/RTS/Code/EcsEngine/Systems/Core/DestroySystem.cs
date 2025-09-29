using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using Client.Components.Common;
using Client.Components.Health;
using Client.Components.Targeting;
using Client.Systems.Spatial;

namespace Client.Systems {

    internal sealed class DestroySystem : IEcsRunSystem {

        private readonly EcsFilterInject<
            Inc<DeathEvent, Inactive>> _filter;

        private readonly EcsCustomInject<EntityManager> _entityManager;
        private readonly EcsCustomInject<SpatialTargetingGridSystem> _spatialGrid;
        private readonly EcsPoolInject<TargetableComponent> _targetablePool;

        public void Run(IEcsSystems systems) {
            var deathEventPool = _filter.Pools.Inc1;
            var inactivePool = _filter.Pools.Inc2;

            foreach (int entity in _filter.Value) {

                // Помечаем как неактивную цель перед удалением
                if (_targetablePool.Value.Has(entity)) {
                    ref var targetable = ref _targetablePool.Value.Get(entity);
                    targetable.IsActive = false;
                    // Не устанавливаем обратно, т.к. entity будет удален
                }

                // Удаляем из spatial grid
                _spatialGrid.Value.RemoveEntity(entity);

                deathEventPool.Del(entity);
                inactivePool.Del(entity);

                //Debug.Log($"DestroySystem: {entity}");

                _entityManager.Value.Destroy(entity);
            }
        }
    }
}

