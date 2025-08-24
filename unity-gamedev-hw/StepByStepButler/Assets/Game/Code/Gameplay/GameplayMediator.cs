using System;
using Zenject;
using UnityEngine;
using UI.Core;
using UI.Components.Screens;
using EventBusService;
using Cysharp.Threading.Tasks;
using StepByStepButler.Gameplay;
using StepByStepButler.Gameplay.Systems;

namespace StepByStepButler.Core {

    public sealed class GameplayMediator : IGameSystem {
        private IEventBus _eventBus;
        private UIManager _uiManager;

        [Inject]
        public void Construct(IEventBus eventBus,
                              UIManager uiManager) {

            _eventBus = eventBus;
            _uiManager = uiManager;
        }

        public void OnInitializeGame() {
            _eventBus.Subscribe<GameOverEvent>(OnGameOverEvent);
        }

        public void OnRestartGame() {
            _eventBus.Unsubscribe<GameOverEvent>(OnGameOverEvent);
            _eventBus.Dispose();
        }

        private void OnGameOverEvent(GameOverEvent @event) {
            ShowGameoverScreen(@event.Winner).Forget();
        }

        private async UniTask ShowGameoverScreen(PlayerType winner) {
            try {
                var gameoverScreen = await _uiManager.ShowScreenAsync<GameoverScreen>(
                    UIScreenType.GameoverScreen
                );

                gameoverScreen.UpdateWinnerLabel($"{winner}");
                Debug.Log($"Game Over! Winner: {winner}");
            }
            catch (Exception e) {
                Debug.LogError($"Failed to show gameover screen: {e.Message}");
            }
        }

        public void OnFinishGame() {
            _eventBus.Unsubscribe<GameOverEvent>(OnGameOverEvent);
            _uiManager = null;
        }
    }
}