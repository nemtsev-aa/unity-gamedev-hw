using R3;
using System;

namespace SessionTrackerSystem {

    public interface ISessionDataViewModel : IViewModel {
        ReadOnlyReactiveProperty<DateTime> LoginTime { get; }
        ReadOnlyReactiveProperty<DateTime> LogoutTime { get; }
        ReadOnlyReactiveProperty<TimeSpan> Duration { get; }
    }
}
