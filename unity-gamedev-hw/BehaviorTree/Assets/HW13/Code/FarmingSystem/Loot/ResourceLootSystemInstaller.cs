using System;
using UnityEngine;
using Zenject;

namespace FarmingSystem {

    [Serializable]
    public sealed class ResourceLootSystemInstaller {
        [SerializeField] private ResourceLootPrefabs _prefabs;
        [SerializeField] private float _spawnRadius = 1f;

        public void Install(DiContainer container) {

            container.BindInstance(_prefabs)
                .AsSingle()
                .NonLazy();

            container.Bind<ResourceLootFactory>()
                .AsSingle()
                .NonLazy();

            container.Bind<ResourceLootSpawner>()
                .AsSingle()
                .WithArguments(_spawnRadius)
                .NonLazy();
        }
    }
}
