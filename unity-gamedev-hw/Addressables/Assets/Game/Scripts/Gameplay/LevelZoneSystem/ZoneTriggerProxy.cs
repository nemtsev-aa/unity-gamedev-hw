using UnityEngine;

namespace LevelZoneSystem {
    public sealed class ZoneTriggerProxy : MonoBehaviour {
        [field: SerializeField] public TransitionTrigger ZoneTrigger { get; private set; }
    }
}