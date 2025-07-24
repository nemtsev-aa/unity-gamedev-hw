using System;
using UnityEngine;
using Game.GameEngine.GameResources;

namespace FarmingSystem {

    [Serializable]
    public class ResourceLootPrefab {
        [field: SerializeField] public ResourceType Type { get; private set; }
        [field: SerializeField] public ResourceLoot Loot { get; private set; }
    }
}

