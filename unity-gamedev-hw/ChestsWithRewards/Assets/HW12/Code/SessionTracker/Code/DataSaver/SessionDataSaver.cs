using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SessionTrackerSystem {

    public sealed class SessionDataSaver : ISessionDataSaver {
        private const string SESSION_ENTRIES_KEY = "SessionEntries";
        private List<SessionData> _sessionEntries = new List<SessionData>();

        public bool TryLoadSessionData(out List<SessionData> data) {
            try {
                if (PlayerPrefs.HasKey(SESSION_ENTRIES_KEY)) {
                    string jsonData = PlayerPrefs.GetString(SESSION_ENTRIES_KEY);

                    if (!string.IsNullOrEmpty(jsonData)) {
                        // Десериализуем сначала в обертку
                        var wrapper = JsonConvert.DeserializeObject<SessionDataWrapper>(jsonData);

                        if (wrapper != null && wrapper.Sessions != null) {
                            _sessionEntries = wrapper.Sessions;
                            data = _sessionEntries;
                            Debug.Log($"<color=green>SessionDataSaver: Loaded {_sessionEntries.Count} sessions successfully!</color>");
                            return true;
                        }
                    }
                }

                data = new List<SessionData>();
                return false;
            }
            catch (Exception e) {
                Debug.LogError($"<color=red>SessionDataSaver: Load failed - {e.Message}</color>");
                data = new List<SessionData>();
                return false;
            }
        }

        public bool TrySaveSessionData(SessionData newData) {
            try {
                // Загружаем текущие данные перед добавлением новых
                TryLoadSessionData(out _sessionEntries);

                _sessionEntries.Add(newData);

                var wrapper = new SessionDataWrapper { Sessions = _sessionEntries };
                string jsonData = JsonConvert.SerializeObject(wrapper,
                    new JsonSerializerSettings {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });

                PlayerPrefs.SetString(SESSION_ENTRIES_KEY, jsonData);
                PlayerPrefs.Save();

                Debug.Log($"<color=green>SessionDataSaver: Saved session successfully! Total sessions: {_sessionEntries.Count}</color>");
                return true;
            }
            catch (Exception e) {
                Debug.LogError($"<color=red>SessionDataSaver: Save failed - {e.Message}</color>");
                return false;
            }
        }

        public bool TryClearSessionData() {
            try {
                _sessionEntries.Clear();
                PlayerPrefs.DeleteKey(SESSION_ENTRIES_KEY);
                PlayerPrefs.Save();

                Debug.Log("<color=green>SessionDataSaver: Cleared all session data successfully!</color>");
                return true;
            }
            catch (Exception e) {
                Debug.LogError($"<color=red>SessionDataSaver: Clear failed - {e.Message}</color>");
                return false;
            }
        }
    }
}

