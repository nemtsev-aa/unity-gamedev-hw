using System;
using System.IO;
using Newtonsoft.Json;

namespace SaveSystem {

    public sealed class JsonToFileStorageService : IStorageService {
        private const string FILE_EXTENSION = ".json";
        private readonly Logger _logger;
        private string _path;

        public JsonToFileStorageService(Logger logger) {
            _logger = logger;
        }

        public void Init(string path) => _path = path;

        public void Save(string key, object data, Action<bool> callback = null) {
            string savePath = BuildPath(key);

            if (File.Exists(savePath) == false) {
                string directory = Path.GetDirectoryName(savePath);
                Directory.CreateDirectory(directory);
            }

            string json = JsonConvert.SerializeObject(data);

            using (var fileStream = new StreamWriter(savePath)) {
                fileStream.Write(json);
                fileStream.Close();
            }

            callback?.Invoke(true);
        }

        public void Load<T>(string key, Action<T> callback) where T : new() {
            string savePath = BuildPath(key);
            _logger.Log($"[{nameof(JsonToFileStorageService)}]: Path {savePath}");
            _logger.Log($"PlayerProgress Loading...");

            if (File.Exists(savePath) == true) {

                using (var fileStream = new StreamReader(savePath)) {
                    var json = fileStream.ReadToEnd();
                    var data = JsonConvert.DeserializeObject<T>(json);

                    callback.Invoke(data);
                    return;
                }
            }

            callback.Invoke(new T());
        }

        private string BuildPath(string key) {
            return Path.Combine(_path, key + FILE_EXTENSION);
        }
    }
}
