using System;
using UnityEngine;

namespace AtomicFramework.AttackCompanent {

    [Serializable]
    public sealed class AttackConfig {
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public float Delay { get; private set; }
        [field: SerializeField] public float Range { get; private set; }
    }
}
