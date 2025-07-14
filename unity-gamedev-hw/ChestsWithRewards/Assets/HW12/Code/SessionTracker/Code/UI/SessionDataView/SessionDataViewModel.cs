using R3;
using System;

namespace SessionTrackerSystem {
    
    public sealed class SessionDataViewModel : ISessionDataViewModel {

        public ReadOnlyReactiveProperty<DateTime> LoginTime { get; }
        public ReadOnlyReactiveProperty<DateTime> LogoutTime { get; }
        public ReadOnlyReactiveProperty<TimeSpan> Duration { get; }

        public SessionDataViewModel(ReactiveSessionData data) {
            LoginTime = data.LoginTime;
            LogoutTime = data.LogoutTime;
            Duration = data.Duration;
        }
    }
}

