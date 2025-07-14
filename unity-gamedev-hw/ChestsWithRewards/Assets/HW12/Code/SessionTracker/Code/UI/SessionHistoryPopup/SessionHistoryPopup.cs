using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SessionTrackerSystem {

    public sealed class SessionHistoryPopup : MonoBehaviour, IDisposable {
        [SerializeField] private Button _closeButton;
        [SerializeField] private SessionDataView _viewPrefab;
        [SerializeField] private RectTransform _container;

        private ISessionHistoryPopupViewModel _viewModel;
        private List<SessionDataView> _views = new List<SessionDataView>();

        public void Init(ISessionHistoryPopupViewModel viewModel) {
            _viewModel = viewModel;
            _viewModel.ModelChanged += CreateViews;

            _closeButton.onClick.AddListener(OnCloseButtonClick);
        }

        public void Show(bool status) {
            gameObject.SetActive(status);

            if (status == true)
                CreateViews();
        }

        private void CreateViews() {
            var sessionDatas = _viewModel.AllSessionsData;

            if (sessionDatas.Count == 0)
                return;

            if (_views.Count > 0)
                ClearViews();

            foreach (var iData in sessionDatas) {
                CreateView(iData);
            }
        }

        private void CreateView(SessionData data) {
            SessionDataView view = Instantiate(_viewPrefab, _container);
            ReactiveSessionData reactiveSessionData = new ReactiveSessionData(data);
            SessionDataViewModel viewModel = new SessionDataViewModel(reactiveSessionData);
            view.Init(viewModel);

            _views.Add(view);
        }

        private void ClearViews() {

            foreach (var iView in _views) {
                iView.Dispose();
                Destroy(iView.gameObject);
            }

            _views.Clear();
        }

        private void OnCloseButtonClick() {
            Show(false);
        }

        public void Dispose() {
            if (_viewModel != null)
                _viewModel.ModelChanged -= CreateViews;

            _closeButton.onClick.RemoveListener(OnCloseButtonClick);
        }
    }
}
