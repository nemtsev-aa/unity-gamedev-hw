using System;
using UnityEngine;

namespace Tutorial.Core {

    [Serializable]
    public sealed class KillEnemyStepController_Config {
        [field: SerializeField] public Transform Target { get; private set; }
    }
}