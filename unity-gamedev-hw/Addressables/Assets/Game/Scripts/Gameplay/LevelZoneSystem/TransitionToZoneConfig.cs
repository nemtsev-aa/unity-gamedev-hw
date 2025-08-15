using System;
using UnityEngine;

namespace LevelZoneSystem {

    [Serializable]
    public sealed class TransitionToZoneConfig {
        [field: SerializeField] public int StartZone { get; private set; }
        [field: SerializeField] public int FinishZone { get; private set; }
    }
}