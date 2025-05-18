using System;
using UnityEngine;

namespace AtomicFramework.CollisionMechanics {

    [Serializable]
    public sealed class CollisionConfig {
        [field: SerializeField] public float Radius { get; private set; }
        [field: SerializeField] public float ScanInterval { get; private set; }
        [field: SerializeField] public LayerMask LayerMask { get; private set; }
    }
}