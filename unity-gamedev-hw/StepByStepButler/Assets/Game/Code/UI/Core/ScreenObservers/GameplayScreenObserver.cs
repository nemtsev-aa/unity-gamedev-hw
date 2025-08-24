using R3;
using UI.Core;
using UI.Components.Screens;
using GameCycleSystem;
using Cysharp.Threading.Tasks;

namespace UI.Services.Observers {

    public sealed class GameplayScreenObserver : IGameFinishListener {
        private readonly GameCycle _gameCycle;
        private readonly UIManager _uiManager;

        private readonly CompositeDisposable _disposables = new();
        private GameplayScreen _gameplayScreen;
        private PauseScreen _pauseScreen;

        public GameplayScreenObserver(GameCycle gameCycle,
                                      UIManager uiManager) {

            _gameCycle = gameCycle;
            _uiManager = uiManager;

            CreateSubscribes();
        }

        private void CreateSubscribes() {

            _uiManager.ScreenOpened
                .Where(screenType => screenType == UIScreenType.GameplayScreen)
                .Subscribe(OnGameplayScreenOpened)
                .AddTo(_disposables);

            _uiManager.ScreenClosed
                .Where(screenType => screenType == UIScreenType.GameplayScreen)
                .Subscribe(OnGameplayScreenClosed)
                .AddTo(_disposables);
        }

        private void OnGameplayScreenOpened(UIScreenType type) {
            _gameplayScreen = _uiManager.GetActiveScreen<GameplayScreen>(type);

            _gameplayScreen.PauseButtonClicked
                .Subscribe(OnPauseButtonClicked)
                .AddTo(_disposables);
        }

        private void OnPauseButtonClicked(Unit _) {
            _uiManager.ShowScreenAsync<PauseScreen>(UIScreenType.PauseScreen).Forget();
            _gameCycle.PauseGame();
        }

        private void OnGameplayScreenClosed(UIScreenType type) {

            if (_gameplayScreen == null)
                _gameplayScreen = _uiManager.GetActiveScreen<GameplayScreen>(type);
        }

        public void OnFinishGame() {

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();

            _gameplayScreen = null;
            _pauseScreen = null;
        }
    }
}