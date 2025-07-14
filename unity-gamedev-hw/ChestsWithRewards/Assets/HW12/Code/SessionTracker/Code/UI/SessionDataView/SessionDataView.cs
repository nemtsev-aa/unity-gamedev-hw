using R3;
using System;
using TMPro;
using UnityEngine;

namespace SessionTrackerSystem {

    public sealed class SessionDataView : MonoBehaviour, IDisposable {
        [SerializeField] private TMP_Text _loginDataTimeText;
        [SerializeField] private TMP_Text _logoutDataTimeText;
        [SerializeField] private TMP_Text _sessionTimeText;

        private CompositeDisposable _disposables = new CompositeDisposable();
        private ISessionDataViewModel _viewModel;

        public void Init(ISessionDataViewModel viewModel) {
            _viewModel = viewModel;
            CreateReactiveSubscribes();          
        }

        private void CreateReactiveSubscribes() {

            _viewModel.LoginTime
                .Subscribe(UpdateLoginDataTime)
                .AddTo(_disposables);

            _viewModel.LogoutTime
                .Subscribe(UpdateLogoutDataTime)
                .AddTo(_disposables);

            _viewModel.Duration
                .Subscribe(UpdateDurationTime)
                .AddTo(_disposables);
        }

        private void UpdateLoginDataTime(DateTime time) {
            _loginDataTimeText.text = $"{time:dd.MM.yyyy HH:mm:ss}";
        }

        private void UpdateLogoutDataTime(DateTime time) {

            if (time == DateTime.MaxValue) {
                _logoutDataTimeText.text = "-";
                return;
            }
                
            _logoutDataTimeText.text = $"{time:dd.MM.yyyy HH:mm:ss}";
        }

        private void UpdateDurationTime(TimeSpan data) {
            _sessionTimeText.text = $"{FormatDuration(data)}";
        }

        private string FormatDuration(TimeSpan duration) {
            return $"{(int)duration.TotalHours:00}:{duration.Minutes:00}:{duration.Seconds:00}";
        }

        public void Dispose() {

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();
        }
    }
}

