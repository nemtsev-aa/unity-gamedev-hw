using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace UpgradesSystem.UI {

    public sealed class UpgradesPopupView : MonoBehaviour {
        [SerializeField] private RectTransform _container;
        [SerializeField] private UpgradeView _viewPrefab;
        [SerializeField] private Button _hideButton;

        private List<UpgradeView> _views = new();

        public void Init(UpgradesPopupViewModel viewModel) {

            for (var index = 0; index < viewModel.UpgradePresenters.Count; index++) {
                var upgradeViewModel = viewModel.UpgradePresenters[index];
                var upgradeView = Instantiate(_viewPrefab, _container);
                upgradeView.Init(upgradeViewModel);

                _views.Add(upgradeView);
            }

            _hideButton.onClick.AddListener(() => Show(false));
        }

        public void Show(bool status) {
            gameObject.SetActive(status);
        }

        public void Dispose() {

            for (var index = 0; index < _views.Count; index++) {
                var view = _views[index];
                view.Dispose();

                Destroy(view.gameObject);
            }

            _views.Clear();
            _hideButton.onClick.RemoveListener(() => Show(false));
        }
    }
}

