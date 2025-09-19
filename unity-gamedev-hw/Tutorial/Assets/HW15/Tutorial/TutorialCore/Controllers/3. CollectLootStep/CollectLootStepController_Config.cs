using System;
using UnityEngine;

namespace Tutorial.Core {

    [Serializable]
    public sealed class CollectLootStepController_Config {
        [field: SerializeField] public Transform Target { get; private set; }
        [field: SerializeField] public int LumberMaxCount { get; internal set; } 
    }
}