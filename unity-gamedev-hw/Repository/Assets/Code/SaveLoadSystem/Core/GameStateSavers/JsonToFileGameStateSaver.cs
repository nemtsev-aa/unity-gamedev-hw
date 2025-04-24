using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SaveLoadSystem {

    public class JsonToFileGameStateSaver : IGameStateSaver {
        private const string FILE_EXTENSION = ".json";
        private string GAME_STATE_KEY = StringConstants.GAME_STATE_KEY;
        private string SAVE_PATH = StringConstants.SAVE_PATH;

        public bool SaveData(Dictionary<string, string> data) {
            if (data == null) {
                Debug.LogError("Cannot save - data dictionary is null");
                return false;
            }

            string savePath = BuildPath(GAME_STATE_KEY);

            try {
                string directory = Path.GetDirectoryName(savePath);

                if (Directory.Exists(directory) == false)
                    Directory.CreateDirectory(directory);

                var unpackedData = new Dictionary<string, object>();
                foreach (var kvp in data) {
                    unpackedData[kvp.Key] = JsonConvert.DeserializeObject<object>(kvp.Value);
                }

                string json = JsonConvert.SerializeObject(unpackedData);
                string tempPath = savePath + ".tmp";

                File.WriteAllText(tempPath, json);

                if (File.Exists(savePath))
                    File.Replace(tempPath, savePath, savePath + ".backup");
                else
                    File.Move(tempPath, savePath);

#if UNITY_EDITOR
                Debug.Log($"JSON data saved successfully to: {savePath}\n" +
                        $"Entries: {data.Count}\n" +
                        $"Size: {new FileInfo(savePath).Length} bytes");
#endif

                return true;
            }
            catch (JsonSerializationException jse) {
                Debug.LogError($"JSON serialization failed: {jse.Message}");
            }
            catch (IOException ioe) {
                Debug.LogError($"File operation error: {ioe.Message}");
            }
            catch (UnauthorizedAccessException uae) {
                Debug.LogError($"Permission denied: {uae.Message}");
            }
            catch (Exception ex) {
                Debug.LogError($"Unexpected error saving data: {ex.Message}");
            }

            return false;
        }

        public Dictionary<string, string> LoadData() {
            string savePath = BuildPath(GAME_STATE_KEY);

            if (File.Exists(savePath) == false) {
                Debug.LogWarning($"Save file not found at path: {savePath}");
                return new Dictionary<string, string>();
            }

            try {
                using (var fileStream = new StreamReader(savePath)) {

                    var json = fileStream.ReadToEnd();

                    if (string.IsNullOrWhiteSpace(json)) {
                        Debug.LogWarning($"Save file is empty: {savePath}");

                        return new Dictionary<string, string>();
                    }

                    if (IsValidJson(json) == false) {
                        Debug.LogError($"Invalid JSON format in file: {savePath}");

                        return new Dictionary<string, string>();
                    }

                    var loadedData = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

                    var result = new Dictionary<string, string>();
                    foreach (var kvp in loadedData) {
                        result[kvp.Key] = JsonConvert.SerializeObject(kvp.Value);
                    }

                    return result;
                }
            }
            catch (Exception ex) {
                Debug.LogError($"Error loading save file: {ex.Message}");

                return new Dictionary<string, string>();
            }
        }

        private string BuildPath(string key) {
            return Path.Combine(SAVE_PATH, key + FILE_EXTENSION);
        }

        private bool VerifySavedData(string path, Dictionary<string, string> originalData) {
            try {
                string savedJson = File.ReadAllText(path);
                var savedData = JsonConvert.DeserializeObject<Dictionary<string, string>>(savedJson);

                return savedData != null && savedData.Count == originalData.Count;
            }
            catch {
                return false;
            }
        }

        private bool IsValidJson(string json) {
            try {
                JToken.Parse(json);
                return true;
            }
            catch {
                return false;
            }
        }
    }
}
