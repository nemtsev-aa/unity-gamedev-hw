using UnityEngine;

namespace FarmingSystem {

    public sealed class LootProxy : MonoBehaviour {
        [field: SerializeField] public Loot Loot { get; private set; }
    }
}
