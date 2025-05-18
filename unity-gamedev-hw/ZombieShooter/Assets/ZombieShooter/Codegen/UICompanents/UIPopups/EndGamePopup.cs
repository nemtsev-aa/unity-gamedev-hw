using Atomic.Elements;
using UnityEngine;
using UnityEngine.UI;
using ZombieShooter.GameCycleSystem;

namespace ZombieShooter.UI {

    public sealed class EndGamePopup : UIPopup {
        [SerializeField] private Button _restartButton;

        private EndGamePopupViewModel _viewModel;
        private IEvent<GameStates> _gameStateChangeAction;

        public void Init(EndGamePopupViewModel viewModel) {
            _viewModel = viewModel;
            _gameStateChangeAction = _viewModel.GameStateChangeAction;

            _restartButton.onClick.AddListener(RestartButtonClick);
        }

        public override void Show(bool status) {
            gameObject.SetActive(status);
        }

        private void RestartButtonClick() {
            Show(false);

            _gameStateChangeAction?.Invoke(GameStates.WaitingToStart);
        }

        public override void Dispose() {
            _restartButton.onClick.RemoveListener(RestartButtonClick);
        }
    }
}
