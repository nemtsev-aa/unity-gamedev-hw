using UnityEngine;
using System;

namespace AtomicFramework.LifeCompanent {

    [Serializable]
    public sealed class LifeConfig {
        [field: SerializeField] public int HitPointCount { get; private set; }
    }
}
