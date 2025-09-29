using System;
using Leopotam.EcsLite;
using System.Collections.Generic;

namespace Client.Services {

    public sealed class EcsWorldsService : IDisposable {
        private readonly Dictionary<string, EcsWorld> _worlds = new();

        public EcsWorldsService() {
            // Автоматически создаём основной мир и мир событий при инициализации сервиса
            _worlds[EcsWorlds.DEFAULT] = new EcsWorld();
            _worlds[EcsWorlds.EVENTS] = new EcsWorld();
        }

        public EcsWorld GetWorld(string worldName = null) {
            worldName ??= EcsWorlds.DEFAULT;

            if (_worlds.TryGetValue(worldName, out EcsWorld world)) {
                return world;
            }

            throw new Exception($"World with name '{worldName}' is not found!");
        }

        public void AddWorld(string worldName, EcsWorld world) {
            if (_worlds.ContainsKey(worldName)) {
                throw new Exception($"World with name '{worldName}' already exists!");
            }

            _worlds[worldName] = world;
        }

        public void Dispose() {
            foreach (var world in _worlds.Values) {
                world?.Destroy();
            }
            _worlds.Clear();
        }
    }
}