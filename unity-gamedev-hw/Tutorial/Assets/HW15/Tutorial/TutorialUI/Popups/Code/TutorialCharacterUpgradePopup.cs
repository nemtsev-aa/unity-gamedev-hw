using R3;
using UnityEngine;
using UpgradesSystem.UI;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace Tutorial.UI {

    public sealed class TutorialCharacterUpgradePopup : Popup, IUpgradesPopupView {
        public GameObject GameObject => gameObject;
        public TutorialPointerUI Pointer => _pointer;

        [Space, SerializeField] private TutorialPointerUI _pointer;
        [SerializeField] private RectTransform _container;
        [SerializeField] private UpgradeView _viewPrefab;

        private List<UpgradeView> _views = new();

        public void Init(UpgradesPopupViewModel viewModel) {
            base.Init();

            for (var index = 0; index < viewModel.UpgradePresenters.Count; index++) {
                var upgradeViewModel = viewModel.UpgradePresenters[index];
                var upgradeView = Instantiate(_viewPrefab, _container);
                upgradeView.Init(upgradeViewModel);

                _views.Add(upgradeView);
            }

            CloseButtonClicked
                .Subscribe(OnCloseButtonClicked)
                .AddTo(Disposables);
        }

        private void OnCloseButtonClicked(Unit _) =>
            Show(false);

        public override void Dispose() {
            base.Dispose();

            for (var index = 0; index < _views.Count; index++) {
                var view = _views[index];
                view.Dispose();

                Destroy(view.gameObject);
            }

            _views.Clear();
        }
    }
}


