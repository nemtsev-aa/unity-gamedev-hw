using R3;
using System;
using UI.Core;
using UI.Components.Screens;
using Cysharp.Threading.Tasks;
using StepByStepButler.Core;
using SceneManagementSystem;

namespace UI.Services.Observers {

    public sealed class MenuScreenObserver : IDisposable {
        private readonly UIManager _uiManager;
        private readonly ISceneLoader _sceneLoader;
        private readonly ApplicationExiter _exiter;

        private readonly CompositeDisposable _disposables = new();
        private MenuScreen _menuScreen;

        public MenuScreenObserver(UIManager uiManager,
                                  ISceneLoader sceneLoader,
                                  ApplicationExiter exiter) {

            _uiManager = uiManager;
            _sceneLoader = sceneLoader;
            _exiter = exiter;

            CreateSubscribes();
        }

        private void CreateSubscribes() {

            _uiManager.ScreenOpened
                .Where(screenType => screenType == UIScreenType.MenuScreen)
                .Subscribe(OnMenuScreenOpened)
                .AddTo(_disposables);
        }

        private void OnMenuScreenOpened(UIScreenType type) {
            _menuScreen = _uiManager.GetActiveScreen<MenuScreen>(type);

            _menuScreen.StartButtonClicked
                .Subscribe(OnStartButtonClicked)
                .AddTo(_disposables);

            _menuScreen.ExitButtonClicked
                .Subscribe(OnExitButtonClicked)
                .AddTo(_disposables);
        }

        private void OnStartButtonClicked(Unit _) {
            _uiManager.HideScreenAsync(UIScreenType.MenuScreen).Forget();
            _sceneLoader.LoadGame();
            _uiManager.ShowScreenAsync<GameplayScreen>(UIScreenType.GameplayScreen).Forget();
        }

        private void OnExitButtonClicked(Unit _) {
            _uiManager.HideScreenAsync(UIScreenType.MenuScreen).Forget();
            _exiter.ExitApp();
        }

        public void Dispose() {

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();
        }
    }
}