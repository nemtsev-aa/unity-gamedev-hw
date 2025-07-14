using System;
using System.Collections.Generic;

namespace SessionTrackerSystem {

    public class SessionHistoryPopupViewModel : ISessionHistoryPopupViewModel {
        private readonly SessionTracker _model;
        private IReadOnlyList<SessionData> _sessions;

        public SessionHistoryPopupViewModel(SessionTracker model) {
            _model = model;
            _sessions = _model.Sessions;

            _model.OnSessionsUpdated += HandleSessionsUpdated;
        }

        public event Action ModelChanged;
        public SessionData LastSessionData => _model.LastSession;
        public IReadOnlyList<SessionData> AllSessionsData => _sessions;

        private void HandleSessionsUpdated() {
            _sessions = _model.Sessions;

            ModelChanged?.Invoke();
        }

        public void Dispose() {
            _model.OnSessionsUpdated -= HandleSessionsUpdated;
        }
    }
}
