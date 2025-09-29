using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Collections.Generic;

namespace Client.Services {

    public sealed class EcsSystemsService : IDisposable {
        private readonly EcsWorldsService _worldsService;
        private readonly Dictionary<string, IEcsSystems> _systems = new();

        public EcsSystemsService(EcsWorldsService worldsService) {
            _worldsService = worldsService;
        }

        public IEcsSystems CreateSystems(string systemsName, EcsWorld world) {
            
            if (_systems.ContainsKey(systemsName) == true) 
                throw new Exception($"Systems with name '{systemsName}' already exists!");
            

            var systems = new EcsSystems(world);
            _systems[systemsName] = systems;

            return systems;
        }

        public IEcsSystems GetSystems(string systemsName = null) {
            systemsName ??= EcsWorlds.DEFAULT;

            if (_systems.TryGetValue(systemsName, out IEcsSystems systems) == true) 
                return systems;
            

            throw new Exception($"Systems with name '{systemsName}' is not found!");
        }

        public void InjectToSystems(object dependency, string systemsName = null) {
            var systems = GetSystems(systemsName);
            systems.Inject(dependency);
        }

        public void InitAll() {

            foreach (var systems in _systems.Values) {
                systems.Init();
            }
        }

        public void RunAll() {

            foreach (var systems in _systems.Values) {
                systems.Run();
            }
        }

        public void DestroyAll() {

            foreach (var systems in _systems.Values) {
                systems.Destroy();
            }

            _systems.Clear();
        }

        public void Dispose() => DestroyAll();
    }
}