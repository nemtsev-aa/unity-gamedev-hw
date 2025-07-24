using System;
using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;

namespace FarmingSystem {

    [Serializable]
    public class ResourceLootSpawner {
        private readonly ResourceLootFactory _lootFactory;
        private readonly float _spawnRadius;

        public ResourceLootSpawner(ResourceLootFactory lootFactory, float spawnRadius) {
            _lootFactory = lootFactory;
            _spawnRadius = spawnRadius;
        }

        public bool TryStartSpawnLoot(ResourceSpot source, out List<Loot> loots) {

            if (source.Resource.Amount == 0) {
                loots = null;
                return false;
            }

            var spawnLoots = new List<Loot>();

            for (int i = 0; i < source.Resource.Amount; i++) {
                Vector3 spawnPosition = source.transform.position + UnityEngine.Random.insideUnitSphere * _spawnRadius;
                spawnPosition.y = 0;

                var config = source.Resource.Config;
                Loot newLoot = _lootFactory.Get(config, source.transform);
                newLoot.Init(config.Type.ToString(), 1);

                newLoot.gameObject.name += $" [{i}]";
                newLoot.transform.SetParent(source.transform.parent);
                newLoot.transform.DOJump(spawnPosition, 1f, 2, 1.5f, false);

                spawnLoots.Add(newLoot);
            }

            loots = spawnLoots;
            return true;
        }
    }
}

