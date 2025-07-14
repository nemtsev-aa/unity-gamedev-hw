using Currencies.UI;
using R3;
using SessionTrackerSystem;
using System;

namespace ChestsSystem {
    [Serializable]
    public sealed class ChestsSystem_UI {
        private readonly ChestsPopup _chestsPopup;
        private readonly ViewModelFactory _chestVMF;
        private readonly CurrencyProvider _currencyProvider;

        private CompositeDisposable _disposables = new();
        private PopupSelectorView _popupSelectorView;

        public ChestsSystem_UI(ChestsPopup chestsPopup,
                               ViewModelFactory chestVMF,
                               CurrencyProvider currencyProvider,
                               UICompanentsPresenter uICompanentsPresenter) {

            _chestsPopup = chestsPopup;
            _chestVMF = chestVMF;
            _currencyProvider = currencyProvider;
            _popupSelectorView = uICompanentsPresenter.PopupSelectorView;

            Init();
        }

        private void Init() {
            _popupSelectorView.ChestsButtonClick
                .Subscribe(ShowChestsPopup)
                .AddTo(_disposables);
        }

        public void ShowChestsPopup(Unit _) {

            var status = !_chestsPopup.gameObject.activeSelf;
            ShowCurrencyView();

            if (status == false) {
                _chestsPopup.Show(false);
                return;
            }

            if (_chestsPopup.IsActive == true) {
                _chestsPopup.Show(true);
                return;
            }

            if (_chestVMF.TryGetViewModel(out IChestsPopupViewModel viewModel) == true) {
                _chestsPopup.Init(viewModel);
                _chestsPopup.Show(true);
            }
        }

        private void ShowCurrencyView() {
            var status = !_currencyProvider.gameObject.activeSelf;
            _currencyProvider.gameObject.SetActive(status);
        }
    }
}
