using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using UnityEngine;

namespace SaveLoadSystem {

    public class BinaryToFileStateSaver : IGameStateSaver {
        private const string FILE_EXTENSION = ".save";
        private string GAME_STATE_KEY = StringConstants.GAME_STATE_KEY;
        private string SAVE_PATH = StringConstants.SAVE_PATH;

        public bool SaveData(Dictionary<string, string> data) {

            if (data == null) {
                Debug.LogError("Cannot save null data dictionary");
                return false;
            }

            string savePath = BuildPath(GAME_STATE_KEY);

            try {

                string directory = Path.GetDirectoryName(savePath);

                if (Directory.Exists(directory) == false)
                    Directory.CreateDirectory(directory);

                string tempPath = savePath + ".tmp";

                using (FileStream fs = new FileStream(tempPath, FileMode.Create)) {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, data);
                }

                if (File.Exists(savePath))
                    File.Replace(tempPath, savePath, savePath + ".backup");
                else
                    File.Move(tempPath, savePath);

                var loadedData = LoadData();

                if (loadedData.SequenceEqual(data) == false) {
                    Debug.LogError("Data integrity check failed after saving");
                    return false;
                }

#if UNITY_EDITOR
                Debug.Log($"Data saved successfully to: {savePath}\n" +
                         $"Entries saved: {data.Count}\n" +
                         $"File size: {new FileInfo(savePath).Length} bytes");
#endif

                return true;
            }
            catch (SerializationException se) {
                Debug.LogError($"Serialization failed: {se.Message}");
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

            if (!File.Exists(savePath)) {
                Debug.LogWarning($"Save file not found at path: {savePath}");
                return new Dictionary<string, string>();
            }

            try {
                using (FileStream fs = new FileStream(savePath, FileMode.Open)) {

                    if (fs.Length == 0) {
                        Debug.LogWarning($"Save file is empty: {savePath}");
                        return new Dictionary<string, string>();
                    }

                    BinaryFormatter formatter = new BinaryFormatter();

                    var data = (Dictionary<string, string>)formatter.Deserialize(fs);

                    return data ?? new Dictionary<string, string>();
                }
            }
            catch (SerializationException se) {
                Debug.LogError($"Serialization error: {se.Message}");
            }
            catch (SecurityException se) {
                Debug.LogError($"Security error: {se.Message}");
            }
            catch (Exception ex) {
                Debug.LogError($"Failed to load save data: {ex.Message}");
            }

            return new Dictionary<string, string>();
        }

        private string BuildPath(string key) {
            return Path.Combine(SAVE_PATH, key + FILE_EXTENSION);
        }
    }
}

