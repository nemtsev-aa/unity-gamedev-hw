using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

namespace SaveLoadSystem {
    public class JsonEncryptionToFileGameStateSaver : IGameStateSaver {
        private const string FILE_EXTENSION = ".json";
        private string GAME_STATE_KEY = StringConstants.GAME_STATE_KEY;
        private string SAVE_PATH = StringConstants.SAVE_PATH;

        private EncryptionSystem _encryption;

        public JsonEncryptionToFileGameStateSaver(EncryptionSystem encryption) {
            _encryption = encryption;
        }

        public bool SaveData(Dictionary<string, string> data) {
            if (data == null) {
                Debug.LogError("Cannot save - data dictionary is null");
                return false;
            }

            string savePath = BuildPath(GAME_STATE_KEY);

            try {
                string directory = Path.GetDirectoryName(savePath);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                string json = JsonConvert.SerializeObject(data);
                byte[] encryptedData = _encryption.EncryptStringToBytes(json);

                string tempPath = savePath + ".tmp";
                File.WriteAllBytes(tempPath, encryptedData);

                if (File.Exists(savePath))
                    File.Replace(tempPath, savePath, savePath + ".backup");
                else
                    File.Move(tempPath, savePath);

#if UNITY_EDITOR
                Debug.Log($"Data saved successfully to: {savePath}\n" +
                         $"Entries: {data.Count}\n" +
                         $"Size: {new FileInfo(savePath).Length} bytes");
#endif

                return true;
            }
            catch (Exception ex) {
                Debug.LogError($"Error saving data: {ex.Message}");
                return false;
            }
        }

        public Dictionary<string, string> LoadData() {
            string savePath = BuildPath(GAME_STATE_KEY);

            if (!File.Exists(savePath)) {
                Debug.LogWarning($"Save file not found at path: {savePath}");
                return new Dictionary<string, string>();
            }

            try {
                byte[] encryptedData = File.ReadAllBytes(savePath);

                try {
                    string json = _encryption.DecryptToString(encryptedData);

                    if (string.IsNullOrWhiteSpace(json) == true) {
                        Debug.LogWarning($"Save file is empty: {savePath}");
                        return new Dictionary<string, string>();
                    }

                    if (IsValidJson(json) == false) {
                        Debug.LogError($"Invalid JSON format in file: {savePath}");
                        return new Dictionary<string, string>();
                    }

                    return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                }
                catch (CryptographicException ex) {
                    Debug.LogError($"Failed to decrypt save file: {ex.Message}");

                    // Попробуем прочитать как незашифрованный файл для совместимости
                    try {
                        string json = File.ReadAllText(savePath);
                        if (!IsValidJson(json))
                            throw;

                        Debug.LogWarning("Loaded unencrypted legacy save file");
                        return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                    }
                    catch {
                        Debug.LogError("File is neither valid encrypted nor unencrypted save");
                        return new Dictionary<string, string>();
                    }
                }
            }
            catch (Exception ex) {
                Debug.LogError($"Error loading save file: {ex.Message}");
                return new Dictionary<string, string>();
            }
        }

        private string BuildPath(string key) => Path.Combine(SAVE_PATH, key + FILE_EXTENSION);

        private bool IsValidJson(string json) {
            try { JToken.Parse(json); return true; }
            catch { return false; }
        }
    }
}
