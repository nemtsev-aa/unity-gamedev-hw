using UnityEngine;

namespace ZombieShooter.UI {

    public sealed class GameplayPopup : UIPopup {
        [SerializeField] private PlayerInfoView _playerInfoView;
        [SerializeField] private EffectViews _effectViews;

        private GameplayPopupViewModel _viewModel;

        public void Init(GameplayPopupViewModel viewModel) {
            _viewModel = viewModel;

            InitViews();
        }

        public override void Show(bool status) {
            gameObject.SetActive(status);
        }

        private void InitViews() {
            _playerInfoView.Init(_viewModel.PlayerInfoViewModel);
            _effectViews.Init();
        }
    }
}
