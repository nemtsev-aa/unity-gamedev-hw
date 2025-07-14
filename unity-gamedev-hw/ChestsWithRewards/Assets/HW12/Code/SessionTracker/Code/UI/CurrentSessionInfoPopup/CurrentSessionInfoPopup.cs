using System;
using UnityEngine;
using UnityEngine.UI;

namespace SessionTrackerSystem {

    public sealed class CurrentSessionInfoPopup : MonoBehaviour, IDisposable {
        [SerializeField] private Button _closeButton;
        [SerializeField] private SessionDataView _view;

        private ISessionDataViewModel _viewModel;

        public void Init(ISessionDataViewModel viewModel) {
            _viewModel = viewModel;
            _closeButton.onClick.AddListener(OnCloseButtonClick);
        }

        public void Show(bool status) {

            if (status == true) 
                _view.Init(_viewModel);
            
            gameObject.SetActive(status);
        }

        private void OnCloseButtonClick() {
            Show(false);
        }

        public void Dispose() {
            _closeButton.onClick.RemoveListener(OnCloseButtonClick);
        }
    }
}
