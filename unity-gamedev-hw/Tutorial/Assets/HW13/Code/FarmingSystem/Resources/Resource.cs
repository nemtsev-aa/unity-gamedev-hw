using System;
using UnityEngine;

namespace FarmingSystem {

    [Serializable]
    public sealed class Resource {
        [field: SerializeField] public ResourceConfig Config { get; private set; }
        [field: SerializeField] public int Amount { get; private set; }

        public Resource(ResourceConfig config, int amount) {
            Config = config;
            Amount = amount;
        }
    }
}



