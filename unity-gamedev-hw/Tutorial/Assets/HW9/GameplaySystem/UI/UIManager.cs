using R3;
using UnityEngine;
using ShopSystem.UI;
using UpgradesSystem.UI;
using ShopUICompanents = ShopSystem.UI.UICompanents;
using UpgradesUICompanents = UpgradesSystem.UI.UICompanents;

namespace GameplaySystem {

    public sealed class UIManager {
        private ToolsView _toolsView;
        private ShopPopupView _shopPopup;
        private SellPopupView _sellPopup;
        private IUpgradesPopupView _upgradesPopup;

        private CompositeDisposable _disposables = new();

        private ShopPopupViewModelFactory _salePVMFactory;
        private SellPopupViewModelFactory _sellPVMFactory;
        private UpgradesPopupViewModelFactory _uPVMFactory;

        private ShopPopupViewModel _shopPopupViewModel;
        private UpgradesPopupViewModel _upgradesPopupViewModel;

        public UIManager(ShopUICompanents shopUICompanents,
                         UpgradesUICompanents upgradesUICompanents,
                         ToolsView toolsView) {

            _shopPopup = shopUICompanents.PopupProvider.ShopPopup;
            _salePVMFactory = shopUICompanents.ViewModelFactories.SaleFactory;

            _sellPopup = shopUICompanents.PopupProvider.SellPopup;
            _sellPVMFactory = shopUICompanents.ViewModelFactories.SellFactory;

            _upgradesPopup = upgradesUICompanents.Popup;
            _uPVMFactory = upgradesUICompanents.ViewModelFactory;

            _toolsView = toolsView;

            CreateReactiveSubscribes();
            InitPopups();
        }

        private void CreateReactiveSubscribes() {
            _toolsView.ShopButtonClicked
                .Subscribe(ShopPopupShow)
                .AddTo(_disposables);

            _toolsView.SellButtonClicked
                .Subscribe(SellPopupShow)
                .AddTo(_disposables);

            _toolsView.UpgradesButtonClicked
                .Subscribe(UpgradesPopupShow)
                .AddTo(_disposables);
        }

        private void InitPopups() {
            _shopPopupViewModel = _salePVMFactory.Get(0);
            _shopPopup.Init(_shopPopupViewModel);

            _upgradesPopupViewModel = _uPVMFactory.Get();
            _upgradesPopup.Init(_upgradesPopupViewModel);
        }

        private void ShopPopupShow(Unit _) {
            _shopPopup.Show(GetShowStatus(_shopPopup.gameObject));
        }

        private void SellPopupShow(Unit _) {
            var status = GetShowStatus(_sellPopup.gameObject);

            if (status == true)
                _sellPopup.Init(_sellPVMFactory.Get());

            _sellPopup.Show(status);
        }

        private void UpgradesPopupShow(Unit _) {
            _upgradesPopup.Show(GetShowStatus(_upgradesPopup.GameObject));
        }

        private bool GetShowStatus(GameObject popup) {
            return !popup.activeSelf;
        }
    }
}