using R3;
using System;
using Newtonsoft.Json;

namespace SessionTrackerSystem {
    
    [Serializable]
    public sealed class ReactiveSessionData {
        private readonly ReactiveProperty<DateTime> _loginTime = new();
        private readonly ReactiveProperty<DateTime> _logoutTime = new();
        private readonly ReactiveProperty<TimeSpan> _duration = new();
        private TimeSpan _totalPauseDuration;

        [JsonIgnore]
        public ReadOnlyReactiveProperty<DateTime> LoginTime => _loginTime;
        [JsonIgnore]
        public ReadOnlyReactiveProperty<DateTime> LogoutTime => _logoutTime;
        [JsonIgnore]
        public ReadOnlyReactiveProperty<TimeSpan> Duration => _duration;

        public ReactiveSessionData() {
            _loginTime.Value = DateTime.Now;
            _logoutTime.Value = DateTime.MaxValue;
            _duration.Value = TimeSpan.Zero;
        }

        public ReactiveSessionData(SessionData data) {
            _loginTime.Value = data.LoginTime;
            _logoutTime.Value = data.LogoutTime;
            _duration.Value = data.Duration;
        }

        public void UpdateLogoutTime() {
            _logoutTime.Value = DateTime.Now;
        }

        public void UpdateDuration() {
            var endTime = _logoutTime.Value != DateTime.MaxValue ? _logoutTime.Value : DateTime.Now;
            _duration.Value = endTime - _loginTime.Value - _totalPauseDuration;
        }

        public void AdjustForPause(TimeSpan pauseDuration) {
            _totalPauseDuration += pauseDuration;
            UpdateDuration();
        }

        public SessionData ToDataModel() {
            
            return new SessionData(
                _loginTime.Value,
                _logoutTime.Value,
                _duration.Value
            );
        }
    }
}

