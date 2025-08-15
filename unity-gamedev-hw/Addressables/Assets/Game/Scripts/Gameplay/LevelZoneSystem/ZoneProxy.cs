using UnityEngine;

namespace LevelZoneSystem {
    public sealed class ZoneProxy : MonoBehaviour {
        [field: SerializeField] public Zone Zone { get; private set; }
    }
}