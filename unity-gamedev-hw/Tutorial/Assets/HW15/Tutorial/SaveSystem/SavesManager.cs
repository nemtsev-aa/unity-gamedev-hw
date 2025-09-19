using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem {

    public sealed class SavesManager : IService, ISaveManager {
        public IStorageService CurrentService => _saveServices[_currentSaveType];

        private readonly string _savePath;
        private readonly Logger _logger;
        private readonly SaveType _currentSaveType;

        private Dictionary<SaveType, IStorageService> _saveServices = new();
        private string _defaultSavePath => Application.persistentDataPath;

        public SavesManager(SaveManagerConfig config, Logger logger) {
            _currentSaveType = config.SaveType;
            _savePath = config.SavePath == "" ? _defaultSavePath : config.SavePath;
            _logger = logger;

            InitializationServices();
        }

        public void Save(string key, object data, Action<bool> callback = null) {
            _saveServices[_currentSaveType].Save(key, data, callback);
        }

        public void Load<T>(string key, Action<T> callback) where T : new() {
            _saveServices[_currentSaveType].Load(key, callback);
        }

        public void DeleteFile(string key) {
            string path = "";

            switch (_currentSaveType) {
                case SaveType.Binary:
                    path = Path.Combine(_savePath, key, ".save");
                    break;

                case SaveType.Json:
                    path = Path.Combine(_savePath, key, ".json");
                    break;

                default:
                    break;
            }

            if (File.Exists(path) == false) {
                _logger.Log("SavesManager: DeleteFile - No file");
                return;
            }

            File.Delete(path);
            _logger.Log("SavesManager: DeleteFile - Deleted");
        }

        private void InitializationServices() {
            _saveServices.Add(SaveType.Binary, new BinaryToFileStorageService(_logger));
            _saveServices.Add(SaveType.Json, new JsonToFileStorageService(_logger));

            foreach (var iService in _saveServices) {
                iService.Value.Init(_savePath);
            }
        }

        public void Dispose() {
            _saveServices.Clear();
        }
    }
}
