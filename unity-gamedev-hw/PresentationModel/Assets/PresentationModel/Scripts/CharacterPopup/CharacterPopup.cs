using R3;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PresentationModel {
    public sealed class CharacterPopup : UIView, IDisposable {
        [SerializeField] private List<UIView> _views;
        [SerializeField] private Button _closeButton;

        private readonly CompositeDisposable _compositeDisposable = new();

        public ICharacterPopupViewModel ViewModel { get; private set; }
        public UserInfoView UserInfoView { get; private set; }
        public PlayerLevelView PlayerLevelView { get; private set; }
        public CharacterInfoView CharacterInfoView { get; private set; }
        public Observable<Unit> OnCloseClickObservable => _closeButton.OnClickAsObservable();

        public override void Init(IViewModel viewModel) {

            if (viewModel is not ICharacterPopupViewModel popupViewModel)
                throw new ArgumentNullException($"Invalid ViewModel: {viewModel}");

            ViewModel = popupViewModel;

            InitViews();
            CreteReactiveSubscribes();
        }

        public void Show() {
            gameObject.SetActive(true);
        }

        public void Hide() {
            gameObject.SetActive(false);
        }

        public override void UpdateCompanents() {
            UserInfoView.UpdateCompanents();
            PlayerLevelView.UpdateCompanents();
            CharacterInfoView.UpdateCompanents();
        }

        private void InitViews() {

            if (TryGetViewByType(out UserInfoView userInfoView)) {
                UserInfoView = userInfoView;
                UserInfoView.Init(ViewModel.UserInfoViewModel);
            }

            if (TryGetViewByType(out PlayerLevelView playerLevelView)) {
                PlayerLevelView = playerLevelView;
                PlayerLevelView.Init(ViewModel.PlayerLevelViewModel);
            }

            if (TryGetViewByType(out CharacterInfoView characterInfoView)) {
                CharacterInfoView = characterInfoView;
                CharacterInfoView.Init(ViewModel.CharacterInfoViewModel);
            }
        }

        private bool TryGetViewByType<T>(out T view) where T : UIView {
            Type targetType = typeof(T);

            for (int i = 0; i < _views.Count; i++) {
                var iView = _views[i];

                if (iView.GetType() == targetType) {
                    view = (T)iView;
                    return true;
                }
            }

            view = null;
            return false;
        }

        private void CreteReactiveSubscribes() {
            OnCloseClickObservable
                .Subscribe(OnCloseClick)
                .AddTo(_compositeDisposable);
        }

        private void OnCloseClick(Unit unit) {
            Hide();
        }

        public void Dispose() {
            if (_compositeDisposable.IsDisposed == false) {
                _compositeDisposable.Dispose();
                ViewModel = null;
            }
        }
    }
}


