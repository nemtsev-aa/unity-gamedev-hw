using UnityEngine;

namespace LevelZoneSystem {

    public sealed class Zone : MonoBehaviour {
        [field: SerializeField] public int Index { get; private set; }
    }
}