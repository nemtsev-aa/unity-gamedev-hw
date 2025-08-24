using Cysharp.Threading.Tasks;
using GameCycleSystem;
using R3;
using SceneManagementSystem;
using UI.Components.Screens;
using UI.Core;
using UnityEngine;

namespace UI.Services.Observers {

    public sealed class GameoverScreenObserver : IGameFinishListener {
        private readonly GameCycle _gameCycle;
        private readonly UIManager _uiManager;
        private readonly ISceneLoader _sceneLoader;

        private CompositeDisposable _disposables;
        private GameoverScreen _gameoverScreen;

        public GameoverScreenObserver(GameCycle gameCycle,
                                      UIManager uiManager,
                                      ISceneLoader sceneLoader) {

            _gameCycle = gameCycle;
            _uiManager = uiManager;
            _sceneLoader = sceneLoader;
            _disposables = new();

            CreateSubscribes();
        }

        private void CreateSubscribes() {

            _uiManager.ScreenOpened
                .Where(screenType => screenType == UIScreenType.GameoverScreen)
                .Subscribe(OnGameplayScreenOpened)
                .AddTo(_disposables);
        }

        private void OnGameplayScreenOpened(UIScreenType type) {
            _gameoverScreen = _uiManager.GetActiveScreen<GameoverScreen>(type);

            _gameoverScreen.RestartButtonClicked
                .Subscribe(OnRestartButtonClicked)
                .AddTo(_disposables);

            _gameoverScreen.MainMenuButtonClicked
                .Subscribe(OnMainMenuButtonClicked)
                .AddTo(_disposables);
        }

        private void OnRestartButtonClicked(Unit _) {
            RestartGameProcess().Forget();
        }

        private async UniTask RestartGameProcess() {
            try {
                await _uiManager.HideScreenAsync(UIScreenType.GameoverScreen, false);
                
                await UniTask.Delay(200);
                _gameCycle.FinishGame();
                
                await UniTask.Delay(100);
                _sceneLoader.LoadGame();
            }
            catch (System.Exception e) {
                Debug.LogError($"Restart failed: {e.Message}");
                _sceneLoader.LoadGame();
            }
        }

        private void OnMainMenuButtonClicked(Unit _) {
            _uiManager.HideScreenAsync(UIScreenType.GameoverScreen).Forget();
            _sceneLoader.LoadMenu();
            _gameCycle.FinishGame();
        }

        public void OnFinishGame() {

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();

            _gameoverScreen = null;
        }
    }
}