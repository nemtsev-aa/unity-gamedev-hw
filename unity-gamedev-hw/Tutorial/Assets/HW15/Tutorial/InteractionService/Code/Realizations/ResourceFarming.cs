using UnityEngine;
using FarmingSystem;

namespace InteractionService {
    public sealed class ResourceFarming : InteractionSource {
        [field: SerializeField] public ResourceSpot ResourceSpot { get; private set; }
    }
}

