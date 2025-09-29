using Client.Components.Targeting;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client.Systems.Spatial {
    public sealed class TargetingInitSystem : IEcsInitSystem {
        private readonly EcsFilterInject<Inc<TargetableComponent>> _targetableFilter;
        private readonly EcsPoolInject<TargetableComponent> _targetablePool;
        private readonly EcsCustomInject<SpatialTargetingGridSystem> _spatialGrid;

        public void Init(IEcsSystems systems) {
            // Инициализируем все targetable entities в spatial grid
            foreach (int entity in _targetableFilter.Value) {
                
                if (_targetablePool.Value.Get(entity).IsActive) {
                    // Spatial grid сам добавит entity при первом обновлении
                }
            }
        }
    }
}

