using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SaveLoadSystem {

    public sealed class PlayerPrefsGameStateSaver : IGameStateSaver {
        private const int PlayerPrefsMaxSize = 1024 * 1024;

        private string GAME_STATE_KEY = StringConstants.GAME_STATE_KEY;

        public bool SaveData(Dictionary<string, string> data) {
            if (data == null) {
                Debug.LogWarning("Attempted to save null data to PlayerPrefs");
                
                return false;
            }

            try {
                string json;
                try {
                    json = JsonConvert.SerializeObject(data);
                }
                catch (JsonException je) {
                    Debug.LogError($"JSON serialization failed: {je.Message}");
                    return false;
                }

                if (json.Length > PlayerPrefsMaxSize) {
                    Debug.LogError($"Data too large for PlayerPrefs ({json.Length} > {PlayerPrefsMaxSize} chars)");
                    return false;
                }

                PlayerPrefs.SetString(GAME_STATE_KEY, json);
                PlayerPrefs.Save();
            

#if UNITY_EDITOR
                Debug.Log($"Saved {data.Count} entries to PlayerPrefs (key: {GAME_STATE_KEY})");
#endif

                return true;
            }
            catch (Exception ex) {
                Debug.LogError($"Failed to save data to PlayerPrefs: {ex.Message}");
                return false;
            }
        }


        public Dictionary<string, string> LoadData() {
            if (PlayerPrefs.HasKey(GAME_STATE_KEY)) {
                var gameStateJson = PlayerPrefs.GetString(GAME_STATE_KEY);
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(gameStateJson);
            }

            Debug.Log("No load!");
            return new Dictionary<string, string>();
        }
    }
}
