using UnityEngine;
using Game.GameEngine.GameResources;

namespace InteractionService {

    public sealed class SellResourceLoot : InteractionSource {
        [field: SerializeField] public ResourceType Type { get; private set; }
    }
}


