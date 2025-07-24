using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using Game.GameEngine.GameResources;

namespace FarmingSystem {

    [Serializable]
    public class ResourceLootPrefabs {
        [field: SerializeField] public List<ResourceLootPrefab> LootPrefabs { get; private set; }

        public ResourceLoot GetPrefabByType(ResourceType type) {
            return LootPrefabs.FirstOrDefault(loot => loot.Type == type).Loot;
        }
    }
}

