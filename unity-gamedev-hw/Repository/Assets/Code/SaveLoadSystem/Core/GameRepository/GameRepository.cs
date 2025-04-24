using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace SaveLoadSystem {

    namespace Core {

        public sealed class GameRepository : IGameRepository {

            private readonly IGameStateSaver _gameStateSaver;
            private Dictionary<string, string> _gameState = new();

            public GameRepository(IGameStateSaver gameStateSaver) {
                _gameStateSaver = gameStateSaver;
            }

            public bool TryGetData<T>(out T data) {
                var key = typeof(T).ToString();

                if (_gameState.TryGetValue(key, out var jsonData)) {
                    data = JsonConvert.DeserializeObject<T>(jsonData);
                    return true;
                }

                data = default;
                return false;
            }

            public void SetData<T>(T data) {
                var jsonData = JsonConvert.SerializeObject(data);
                var key = typeof(T).ToString();

                _gameState[key] = jsonData;
            }

            public void LoadState() {
                _gameState = _gameStateSaver.LoadData();

                string result = (_gameState.Count == 0) ? "failed" : "succeed";
                Debug.Log($"GameRepository: LoadState {result}!");
            }

            public void SaveState() {
                string result = (_gameStateSaver.SaveData(_gameState) == false) ? "failed" : "succeed";
                Debug.Log($"GameRepository: SaveState {result}!");
            }
        }
    }
}

