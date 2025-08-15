using AssetManagementSystem;
using CharactersSystem.Spawner;
using GameCycleSystem;
using System.Collections.Generic;

namespace SampleGame.Core {

    public sealed class GameCycleInitializer {
        private readonly GameCycle _gameCycle;
        private readonly List<IGameListener> _gameListeners;

        public GameCycleInitializer(GameCycle gameCycle,
                                    AssetPreloader assetPreloader,
                                    CharacterSpawner characterSpawner) {

            _gameCycle = gameCycle;

            _gameListeners = new List<IGameListener>() {
                assetPreloader,
                characterSpawner
            };

            AddGameListeners();
        }

        public void AddGameListeners() {

            for (int i = 0; i < _gameListeners.Count; i++) {

                var listener = _gameListeners[i];
                
                if (listener != null)
                    _gameCycle.Add(listener);
            }
        }
    }
}


