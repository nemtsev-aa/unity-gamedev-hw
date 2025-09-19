using System;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

namespace SaveSystem {

    public sealed class BinaryToFileStorageService : IStorageService {
        private const string FILE_EXTENSION = ".save";
        private readonly Logger _logger;
        private string _path;

        public BinaryToFileStorageService(Logger logger) {
            _logger = logger;
        }

        public void Init(string path) => _path = path;

        public void Save(string key, object data, Action<bool> callback = null) {
            string savePath = BuildPath(key);
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate)) {
                formatter.Serialize(fs, data); // Cериализуем весь массив
            }

#if UNITY_EDITOR
            Debug.Log($"Data saved to Binary successfully: {savePath}");
#endif
            callback?.Invoke(true);
        }

        public void Serialize<T>(T obj) {
            BinaryFormatter formatter = new BinaryFormatter();

            using (Stream stream = new MemoryStream()) {
                formatter.Serialize(stream, obj);
                stream.Position = 0;
                T deserializedObj = (T)formatter.Deserialize(stream);
            }
        }

        public void Load<T>(string key, Action<T> callback) where T : new() {
            string savePath = BuildPath(key);
            _logger.Log($"[{nameof(BinaryToFileStorageService)}]: Path {savePath}");
            _logger.Log($"PlayerProgress Loading...");

            if (File.Exists(savePath) == true) {
                BinaryFormatter formatter = new BinaryFormatter();

                using (FileStream fs = new FileStream(savePath, FileMode.Open)) {

                    if (fs.Length > 0) {
                        T data = (T)formatter.Deserialize(fs);
                        callback.Invoke(data);
                    }
                }
            }

            callback?.Invoke(new T());
        }

        private string BuildPath(string key) {
            return Path.Combine(_path, key + FILE_EXTENSION);
        }
    }
}
    
