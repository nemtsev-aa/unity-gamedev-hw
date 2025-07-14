using R3;
using System;

namespace SessionTrackerSystem {
    
    [Serializable]
    public sealed class SessionTracker_UI {
        private readonly UICompanentsPresenter _uICompanentsPresenter;
        private readonly ViewModelFactory _sessionTrackerVMF;
        private readonly PopupSelectorView _popupSelectorView;
        private readonly SessionHistoryPopup _historyPopup;
        private readonly CurrentSessionInfoPopup _currentSessionPopup;

        private CompositeDisposable _disposables = new();

        public SessionTracker_UI(UICompanentsPresenter uICompanentsPresenter, ViewModelFactory sessionTrackerVMF) {
           _uICompanentsPresenter = uICompanentsPresenter;
           _sessionTrackerVMF = sessionTrackerVMF;

            _historyPopup = _uICompanentsPresenter.HistoryPopup;
            _currentSessionPopup = _uICompanentsPresenter.CurrentSessionInfoPopup;
            _popupSelectorView = _uICompanentsPresenter.PopupSelectorView;

            Init();
        }

        private void Init() {
 
            _popupSelectorView.HistoryButtonClick
                .Subscribe(ShowHistoryPopup)
                .AddTo(_disposables);

            _popupSelectorView.CurrentSessionButtonClick
                .Subscribe(ShowCurrentSessionInfoPopup)
                .AddTo(_disposables);
        }

        public void ShowPopupSelectorView() {
            var status = !_popupSelectorView.gameObject.activeSelf;

            if (status == false) {
                _popupSelectorView.Show(false);
                return;
            }

            if (_sessionTrackerVMF.TryGetViewModel(out IPopupSelectorViewModel viewModel) == true) {
                _popupSelectorView.Init(viewModel);
                _popupSelectorView.Show(true);
            }
        }

        public void ShowHistoryPopup(Unit _) {
            var status = !_historyPopup.gameObject.activeSelf;

            if (status == false) {
                _historyPopup.Dispose();
                _historyPopup.Show(false);
                return;
            }

            if (_sessionTrackerVMF.TryGetViewModel(out ISessionHistoryPopupViewModel viewModel) == true) {
                _historyPopup.Init(viewModel);
                _historyPopup.Show(true);
            }
        }

        public void ShowCurrentSessionInfoPopup(Unit _) {

            var status = !_currentSessionPopup.gameObject.activeSelf;

            if (status == false) {
                _currentSessionPopup.Dispose();
                _currentSessionPopup.Show(false);
                return;
            }

            if (_sessionTrackerVMF.TryGetViewModel(out ISessionDataViewModel viewModel) == true) {
                _currentSessionPopup.Init(viewModel);
                _currentSessionPopup.Show(true);
            }
        }
    }
}


