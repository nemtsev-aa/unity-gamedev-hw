using GameCycleSystem;
using StepByStepButler.Core;
using StepByStepButler.Gameplay.Heroes;
using StepByStepButler.Gameplay.Systems;
using StepByStepButler.Gameplay.Systems.Audio;
using System.Collections.Generic;
using UI.Components.ActiveEffectViewSystem;

namespace StepByStepButler.Gameplay {

    public sealed class GameplayInitializer {
        private readonly GameCycle _gameCycle;
        private readonly List<IGameListener> _gameListeners;

        public GameplayInitializer(GameCycle gameCycle,
                                   HeroesProvider heroesProvider,
                                   TurnSystem turnSystem,
                                   CombatSystem combatSystem,
                                   DamageSystem damageSystem,
                                   ViewSystem viewSystem,
                                   AudioSystem audioSystem,
                                   DeathAnimationSystem deathAnimationSystem,
                                   DamagePopupSystem damagePopupSystem,
                                   GameplayMediator gameplayMediator,
                                   ActiveEffectViewSystem activeEffectViewSystem) {

            _gameCycle = gameCycle;

            _gameListeners = new List<IGameListener>() {
                heroesProvider,
                turnSystem,
                combatSystem,
                damageSystem,
                viewSystem,
                audioSystem,
                deathAnimationSystem,
                damagePopupSystem,
                gameplayMediator,
                activeEffectViewSystem
             };

            AddGameListeners();
        }

        public void AddGameListeners() {

            for (int i = 0; i < _gameListeners.Count; i++) {

                var listener = _gameListeners[i];

                if (listener != null)
                    _gameCycle.Add(listener);
            }
        }
    }
}