using Atomic.Elements;
using UnityEngine;
using UnityEngine.UI;
using ZombieShooter.GameCycleSystem;

namespace ZombieShooter.UI {

    public sealed class StartGamePopup : UIPopup {
        [SerializeField] private Button _startButton;

        private StartGamePopupViewModel _viewModel;
        private IEvent<GameStates> _gameStateChangeAction;

        public void Init(StartGamePopupViewModel viewModel) {
            _viewModel = viewModel;
            _gameStateChangeAction = _viewModel.GameStateChangeAction;

            _startButton.onClick.AddListener(StartButtonClick);
        }

        public override void Show(bool status) {
            gameObject.SetActive(status);
        }

        private void StartButtonClick() {
            Show(false);

            _gameStateChangeAction?.Invoke(GameStates.Playing);
        }

        public override void Dispose() {
            _startButton.onClick.RemoveListener(StartButtonClick);
        }
    }
}

