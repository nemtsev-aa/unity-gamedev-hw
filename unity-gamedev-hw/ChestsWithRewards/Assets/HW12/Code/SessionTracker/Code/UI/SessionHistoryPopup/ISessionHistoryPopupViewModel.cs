using System;
using System.Collections.Generic;

namespace SessionTrackerSystem {

    public interface ISessionHistoryPopupViewModel : IViewModel, IDisposable {
        event Action ModelChanged;
        SessionData LastSessionData { get; }
        IReadOnlyList<SessionData> AllSessionsData { get; }
    }
}
