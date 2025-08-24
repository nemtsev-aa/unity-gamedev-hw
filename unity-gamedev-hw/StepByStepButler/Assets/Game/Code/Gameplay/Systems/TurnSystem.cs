using System;
using System.Linq;
using EventBusService;
using System.Collections.Generic;
using StepByStepButler.Gameplay.Heroes;

namespace StepByStepButler.Gameplay.Systems {

    public sealed class TurnSystem : IGameSystem {
        private readonly IEventBus _eventBus;
        private readonly HeroesProvider _heroes;
        private readonly Queue<int> _turnOrder = new();
        private int _currentTurnHero = -1;

        private IReadOnlyList<Entity> Entities => _heroes.GetEntities();

        public TurnSystem(IEventBus eventBus, HeroesProvider heroes) {
            _eventBus = eventBus;
            _heroes = heroes;
        }

        public void OnInitializeGame() {
            SetupTurnOrder();

            _eventBus.Subscribe<ReadyForNextTurnEvent>(OnReadyForNextTurn);
        }

        public void OnFinishGame() {
             _turnOrder.Clear();
            _currentTurnHero = -1;

            _eventBus.Unsubscribe<ReadyForNextTurnEvent>(OnReadyForNextTurn);
        }

        public void OnRestartGame() {
            _turnOrder.Clear();
            _currentTurnHero = -1;
        }

        public void StartNextTurn() {

            if (_turnOrder.Count == 0)
                return;

            _currentTurnHero = _turnOrder.Dequeue();
            var hero = GetCurrentHero();

            if (hero == null || hero.IsAlive() == false) {
                StartNextTurn();
                return;
            }

            if (hero.IsFrozen() == true) {
                hero.ReduceFreezeTurns();
                
                _turnOrder.Enqueue(_currentTurnHero);
                
                StartNextTurn();
                return;
            }

            _turnOrder.Enqueue(_currentTurnHero);
            _eventBus.Publish(new TurnStartedEvent(_currentTurnHero));
        }

        public Entity GetCurrentHero() {

            if (_heroes.TryGetEntityById(_currentTurnHero, out var entity) == false)
                return null;

            return entity;
        }

        private void SetupTurnOrder() {
            var redTeam = Entities
                .Where(h => h.IsRedTeam()).ToList();
            
            var blueTeam = Entities
                .Where(h => h.IsBlueTeam()).ToList();

            for (int i = 0; i < Math.Max(redTeam.Count, blueTeam.Count); i++) {

                if (i < redTeam.Count)
                    _turnOrder.Enqueue(redTeam[i].Id);

                if (i < blueTeam.Count)
                    _turnOrder.Enqueue(blueTeam[i].Id);
            }
        }

        private void OnReadyForNextTurn(ReadyForNextTurnEvent _) {
            StartNextTurn();
        }
    }
}