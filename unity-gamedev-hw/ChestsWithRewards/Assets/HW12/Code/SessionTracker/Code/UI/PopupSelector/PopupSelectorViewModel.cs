using R3;
using GameCycleSystem;

namespace SessionTrackerSystem {

    public sealed class PopupSelectorViewModel : IPopupSelectorViewModel {
        public ReadOnlyReactiveProperty<GameStates> CurrentGameState => _currentGameState;

        private ReactiveProperty<GameStates> _currentGameState;
        private GameCycle _gameCycle;

        public PopupSelectorViewModel(GameCycle gameCycle) {
            _gameCycle = gameCycle;
            _gameCycle.CurrentGameStateChanged += OnCurrentGameStateChanged;

            _currentGameState = new ReactiveProperty<GameStates>(gameCycle.CurrentState);
        }

        private void OnCurrentGameStateChanged(GameStates states) {
            _currentGameState.Value = states;
        }

        public void Dispose() {
            _currentGameState = null;
            _gameCycle.CurrentGameStateChanged -= OnCurrentGameStateChanged;
        }
    }
}


