using System.Collections.Generic;
using StepByStepButler.Gameplay.Heroes;
using StepByStepButler.Gameplay.Systems;

namespace UI.Components.ActiveEffectViewSystem {

    public sealed class ActiveEffectViewSystem : IGameSystem {
        private readonly HeroesProvider _heroesProvider;
        private readonly ActiveEffectViewPool _viewPool;

        private IReadOnlyList<Entity> Entities => _heroesProvider.GetEntities();

        public ActiveEffectViewSystem(HeroesProvider heroesProvider,
                                      ActiveEffectViewPool viewPool) {

            _heroesProvider = heroesProvider;
            _viewPool = viewPool;
        }

        public void OnInitializeGame() {
            CreateSubscribes();
            _viewPool.CreatePool();
            ShowActiveEffectViews();
        }

        public void OnFinishGame() {
            RemoveSubscribes();
            _viewPool.Reset();
        }

        public void OnRestartGame() {
            RemoveSubscribes();
            _viewPool.Reset();
        }

        private void CreateSubscribes() {

            for (int i = 0; i < Entities.Count; i++) {
                var iEntity = Entities[i];

                iEntity.CompanentAdded += OnEntityCompanentAdded;
                iEntity.CompanentRemoved += OnEntityCompanentRemoved;
            }
        }

        private void RemoveSubscribes() {

            for (int i = 0; i < Entities.Count; i++) {
                var iEntity = Entities[i];

                iEntity.CompanentAdded -= OnEntityCompanentAdded;
                iEntity.CompanentRemoved -= OnEntityCompanentRemoved;
            }
        }

        private void ShowActiveEffectViews() {

            for (int i = 0; i < Entities.Count; i++) {
                var iEntity = Entities[i];

                if (iEntity.HasComponent<DivineShieldComponent>() == true)
                    _viewPool.ShowEffectByType(ActiveEffectType.DivineShield, iEntity, true);

                if (iEntity.HasComponent<FrozenComponent>() == true)
                    _viewPool.ShowEffectByType(ActiveEffectType.Freezing, iEntity, true);
            }
        }

        private void OnEntityCompanentAdded(Entity entity, IComponent component) {

            if (component is DivineShieldComponent divineShield) {
                _viewPool.ShowEffectByType(ActiveEffectType.DivineShield, entity, true);
                return;
            }

            if (component is FrozenComponent frozen)
                _viewPool.ShowEffectByType(ActiveEffectType.Freezing, entity, true);
        }

        private void OnEntityCompanentRemoved(Entity entity, IComponent component) {

            if (component is DivineShieldComponent divineShield) {
                _viewPool.ShowEffectByType(ActiveEffectType.DivineShield, entity, false);
                return;
            }

            if (component is FrozenComponent frozen)
                _viewPool.ShowEffectByType(ActiveEffectType.Freezing, entity, false);

            //if (component is DivineShieldComponent divineShield) 
            //    _eventBus.Publish(new DivineShieldStausChangedEvent(entity, false));

            //if (component is FrozenComponent frozen)
            //    _eventBus.Publish(new FreezingEffectStausChangedEvent(entity, false));
        }
    }
}
