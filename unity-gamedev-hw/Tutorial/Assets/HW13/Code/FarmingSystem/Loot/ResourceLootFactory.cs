using System;
using Zenject;
using UnityEngine;

namespace FarmingSystem {

    [Serializable]
    public class ResourceLootFactory {
        private readonly DiContainer _diContainer;
        private readonly ResourceLootPrefabs _prefabs;

        public ResourceLootFactory(DiContainer diContainer, ResourceLootPrefabs prefabs) {
            _diContainer = diContainer;
            _prefabs = prefabs;
        }

        public ResourceLoot Get(ResourceConfig config, Transform parent) {
            var prefab = _prefabs.GetPrefabByType(config.Type);
            var newLoot = _diContainer.InstantiatePrefabForComponent<ResourceLoot>(
                prefab,
                parent.position,
                parent.rotation,
                parent);

            newLoot.Init(new Resource(config, 1));

            return newLoot;
        }
    }
}


