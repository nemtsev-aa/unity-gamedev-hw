using System;
using UnityEngine;

namespace Tutorial.Core {

    [Serializable]
    public sealed class TakeResourceStepController_Config {
        [field: SerializeField] public Transform Target { get; private set; }
        [field: SerializeField] public int FelledWoodMaxCount { get; internal set; }
    }
}