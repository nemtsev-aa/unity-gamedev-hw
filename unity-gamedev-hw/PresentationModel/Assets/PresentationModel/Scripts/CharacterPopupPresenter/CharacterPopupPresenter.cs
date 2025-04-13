using R3;
using System;
using UnityEngine;

namespace PresentationModel {
    public sealed class CharacterPopupPresenter {
        private const int ADDED_EXPERIENCE_AMOUNT = 50;

        private readonly CharacterPopup _popup;
        private readonly CharacterPopupPresenterView _popupPresenterView;
        private readonly CharacterPopupViewModelManager _viewModelManager;
        private readonly RealTimeChangeManager _realTimeChangeManager;

        private readonly CompositeDisposable _compositeDisposable = new();

        private CharacterPopupViewModel _viewModel;

        public CharacterPopupPresenter(CharacterPopup popup,
                                       CharacterPopupPresenterView popupPresenterView,
                                       CharacterPopupViewModelManager viewModelManager,
                                       RealTimeChangeManager realTimeChangeManager) {
            
            _popup = popup;
            _popupPresenterView = popupPresenterView;
            _viewModelManager = viewModelManager;
            _realTimeChangeManager = realTimeChangeManager;

            CreateReactiveSubscribes();
        }

        public CharacterPopupViewModel ViewModel => _viewModel;

        public void SetVewModel(CharacterPopupViewModel viewModel) {
            _viewModel = viewModel;

            Debug.Log($"CharacterPopupViewModel {_viewModel.UserInfoViewModel.UserName} setuped!");
        }

        public void ShowPopup() {
            if (_popup.gameObject.activeSelf == true)
                HidePopup();

            if (_viewModel == null)
                throw new ArgumentNullException($"CharacterPopupViewModel is empty!");

            _popup.Init(_viewModel);
            _popup.Show();
        }

        public void HidePopup() {
            if (_popup.gameObject.activeSelf == true)
                _popup.Hide();
        }

        private void CreateReactiveSubscribes() {
            _viewModelManager.ViewModel
                .Subscribe(OnViewModelCreated)
                .AddTo(_compositeDisposable);

            _popupPresenterView.OnShowPopupClickObservable
                .Subscribe(OnShowPopupClick)
                .AddTo(_compositeDisposable);

            _popupPresenterView.OnAddExperienceClickObservable
                .Subscribe(OnAddExperienceClick)
                .AddTo(_compositeDisposable);

            _popup.OnCloseClickObservable
                .Subscribe(OnPopupCloseClick)
                .AddTo(_compositeDisposable);
        }

        private void OnViewModelCreated(CharacterPopupViewModel viewModel) {
            _viewModel = viewModel;
        }

        private void OnShowPopupClick(Unit unit) {
            ShowPopup();

            _realTimeChangeManager.enabled = true;
            _realTimeChangeManager.Init(_popup);
        }

        private void OnAddExperienceClick(Unit unit) {
            _viewModel.PlayerLevelViewModel.PlayerLevel.AddExperience(ADDED_EXPERIENCE_AMOUNT);
            _popup.UpdateCompanents();
        }

        private void OnPopupCloseClick(Unit unit) {
            _popupPresenterView.Reset();
        }
    }
}


