using System;
using Zenject;
using UnityEngine;
using GameCycleSystem;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using StepByStepButler.Gameplay.Systems;

namespace StepByStepButler.Gameplay {

    public sealed class GameplayBootstrapper : MonoBehaviour, IGameRestartListener {
        private GameCycle _gameCycle;
        private TurnSystem _turnSystem;

        [Inject]
        public void Construct(GameCycle gameCycle,
                              TurnSystem turnSystem) {

            _gameCycle = gameCycle;
            _turnSystem = turnSystem;

            _gameCycle.Add(this);
        }

        private void Start() {
            StartGameplayAsync().Forget();
        }

        public void OnRestartGame() {
            StartGameplayAsync().Forget();
        }

        private async UniTask StartGameplayAsync() {

            if (TryFindSceneContext(out SceneContext sceneContext) == false)
                throw new ArgumentNullException($"GameplayBootstrapper: SceneContext not found!");

            await WaitForSceneContextInitialization(sceneContext);

            _gameCycle.SetCurrentState(GameStates.InitializingComponents);
            _gameCycle.InitializeGame();

            _gameCycle.StartGame();
            _turnSystem.StartNextTurn();
        }

        private bool TryFindSceneContext(out SceneContext sceneContext) {
            var context = FindAnyObjectByType<SceneContext>();

            if (context != null) {
                sceneContext = context;
                return true;
            }

            sceneContext = null;
            return false;
        }

        private async Task WaitForSceneContextInitialization(SceneContext sceneContext) {

            while (sceneContext.HasResolved == false) {
                await Task.Yield();
            }
        }
    }
}