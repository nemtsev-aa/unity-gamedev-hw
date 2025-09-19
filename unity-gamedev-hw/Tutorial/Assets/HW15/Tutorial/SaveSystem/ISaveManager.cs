using System;

namespace SaveSystem {

    public interface ISaveManager {
        IStorageService CurrentService { get; }
        void Save(string key, object data, Action<bool> callback = null);
        void Load<T>(string key, Action<T> callback) where T : new();
        public void DeleteFile(string key);
        public void Dispose();
    }
}
