using UnityEngine;
using FarmingSystem;

namespace InteractionService {
    public sealed class LootCollect : InteractionSource {
        [field: SerializeField] public Loot Loot { get; private set; }
    }
}


