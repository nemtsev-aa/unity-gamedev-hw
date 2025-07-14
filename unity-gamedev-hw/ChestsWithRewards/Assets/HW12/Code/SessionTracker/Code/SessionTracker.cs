using System;
using System.Collections.Generic;
using UnityEngine;
using GameCycleSystem;

namespace SessionTrackerSystem {

    public sealed class SessionTracker : IGameInitializeListener,
                                   IGameStartListener,
                                   IGameFixedUpdateListener,
                                   IGameFinishListener,
                                   IGamePauseListener {

        public event Action OnSessionsUpdated;
        public IReadOnlyList<SessionData> Sessions => _sessionEntries.AsReadOnly();
        public SessionData LastSession => _sessionEntries.Count > 0 ? _sessionEntries[^1] : null;
        public ReactiveSessionData CurrentSession => _currentSessionData;
        public TimeSpan CurrentSessionDuration => _currentSessionData.Duration.CurrentValue;

        private readonly ISessionDataSaver _dataSaver;
        private readonly List<SessionData> _sessionEntries = new();
        private ReactiveSessionData _currentSessionData;
        private bool _isSessionActive;
        private bool _isPaused;
        private DateTime _pauseStartTime;

        public SessionTracker(ISessionDataSaver dataSaver) {
            _dataSaver = dataSaver ?? throw new ArgumentNullException(nameof(dataSaver));
        }

        void IGameInitializeListener.OnInitializeGame() {

            if (_dataSaver.TryLoadSessionData(out var data)) {
                _sessionEntries.AddRange(data);
                OnSessionsUpdated?.Invoke();
            }

            Debug.Log($"<color=orange>[SessionTracker] Initialized</color>");
        }

        void IGameStartListener.OnStartGame() {
            StartNewSession();
        }

        void IGameFixedUpdateListener.OnFixedUpdateGame() {

            if (_isSessionActive == true && _isPaused == false)
                _currentSessionData?.UpdateDuration();
        }

        void IGameFinishListener.OnFinishGame() {
            EndCurrentSession();
            SaveCurrentSession();

            Debug.Log($"<color=orange>[SessionTracker] Game finished </color>");
        }

        void IGamePauseListener.OnPauseGame() {

            if (_isPaused == false) {
                _pauseStartTime = DateTime.Now;
                _isPaused = true;
                Debug.Log($"<color=orange>[SessionTracker] Game paused </color>");

                return;
            }

            var pauseDuration = DateTime.Now - _pauseStartTime;
            _currentSessionData?.AdjustForPause(pauseDuration);
            _isPaused = false;
            Debug.Log($"<color=orange>[SessionTracker] Game resumed after pause </color>");
        }

        private void StartNewSession() {

            if (_isSessionActive == true)
                return;

            _currentSessionData = new ReactiveSessionData();
            _isSessionActive = true;
            _isPaused = false;

            Debug.Log($"<color=orange>[SessionTracker] New session started! </color>");
        }

        private void EndCurrentSession() {
            if (_isSessionActive == false || _currentSessionData == null)
                return;

            _currentSessionData.UpdateLogoutTime();
            _currentSessionData.UpdateDuration();
            _sessionEntries.Add(_currentSessionData.ToDataModel());

            _isSessionActive = false;
            OnSessionsUpdated?.Invoke();
            Debug.Log($"<color=orange>[SessionTracker] Session ended </color>");
        }

        private void SaveCurrentSession() {

            if (_currentSessionData == null)
                return;

            var sessionData = _currentSessionData.ToDataModel();

            if (_dataSaver.TrySaveSessionData(sessionData) == false)
                Debug.LogError($"<color=orange>[SessionTracker] Failed to save session data </color>");
        }
    }
}

