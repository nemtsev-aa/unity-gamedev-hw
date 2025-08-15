using R3;
using System;
using Cysharp.Threading.Tasks;
using GameCycleSystem;
using AssetManagementSystem;
using UI.Components.Screens;
using UI.Core;

namespace UI.Services.Observers {

    public class PauseScreenObserver : IGameFinishListener {
        private readonly GameCycle _gameCycle;
        private readonly UIManager _uiManager;
        private readonly ISceneLoader _sceneLoader;

        private readonly CompositeDisposable _disposables = new();
        private PauseScreen _pauseScreen;

        public PauseScreenObserver(GameCycle gameCycle,
                                   UIManager uiManager,
                                   ISceneLoader sceneLoader) {

            _gameCycle = gameCycle;
            _uiManager = uiManager;
            _sceneLoader = sceneLoader;

            CreateSubscribes();
        }

        private void CreateSubscribes() {

            _uiManager.ScreenOpened
                .Where(screenType => screenType == UIScreenType.PauseScreen)
                .Subscribe(OnPauseScreenOpened)
                .AddTo(_disposables);
        }

        private void OnPauseScreenOpened(UIScreenType type) {
            _pauseScreen = _uiManager.GetActiveScreen<PauseScreen>(type);

            _pauseScreen.ResumeButtonClicked
                .Subscribe(OnResumeButtonClicked)
                .AddTo(_disposables);

            _pauseScreen.ExitButtonClicked
                .Subscribe(OnExitButtonClicked)
                .AddTo(_disposables);
        }

        private void OnResumeButtonClicked(Unit _) {
            _uiManager.HideScreenAsync(UIScreenType.PauseScreen).Forget();
            _gameCycle.PauseGame();
        }

        private void OnExitButtonClicked(Unit _) {
            _uiManager.HideScreenAsync(UIScreenType.PauseScreen).Forget();
            _sceneLoader.LoadMenu();
            _gameCycle.FinishGame();
        }

        public void OnFinishGame() {

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();

            _pauseScreen = null;
        }
    }
}