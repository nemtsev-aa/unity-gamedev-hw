using System;
using System.Collections.Generic;
using StepByStepButler.Gameplay.Systems;

namespace StepByStepButler.Gameplay.Heroes {

    public sealed class HeroesProvider : IGameSystem {
        public IReadOnlyList<Entity> GetEntities() => _entities;

        private readonly EntityFactory _entityFactory;
        private readonly List<Entity> _entities = new();

        public HeroesProvider(EntityFactory entityFactory) {
            _entityFactory = entityFactory;
        }

        public void OnInitializeGame() {
            // Red team heroes
            CreateHero(HeroType.Devourer, PlayerType.Red, 0);
            CreateHero(HeroType.Huntress, PlayerType.Red, 1);
            CreateHero(HeroType.StupidOrc, PlayerType.Red, 2);
            CreateHero(HeroType.LordVamp, PlayerType.Red, 3);

            // Blue team heroes
            CreateHero(HeroType.Paladin, PlayerType.Blue, 0);
            CreateHero(HeroType.IceMage, PlayerType.Blue, 1);
            CreateHero(HeroType.Meditator, PlayerType.Blue, 2);
            CreateHero(HeroType.Electro, PlayerType.Blue, 3);
        }

        public void OnFinishGame() {

            for (int i = 0; i < _entities.Count; i++) {

                if (_entities[i] != null)
                    _entities[i].Dispose();
            }

            _entities.Clear();
        }

        public void OnRestartGame() {
            for (int i = 0; i < _entities.Count; i++) {

                if (_entities[i] != null)
                    _entities[i].Dispose();
            }

            _entities.Clear();

            _entityFactory.ResetIdCounter();
        }

        public bool TryGetEntityById(int id, out Entity entity) {

            for (int i = 0; i < _entities.Count; i++) {
                Entity iEntity = _entities[i];

                if (iEntity.Id == id) {
                    entity = iEntity;
                    return true;
                }
            }

            entity = default;
            return false;
        }

        private void CreateHero(HeroType heroType, PlayerType playerType, int viewIndex) {
            var entity = _entityFactory.CreateHero(heroType, playerType, viewIndex);
            _entities.Add(entity);
        }
    }
}