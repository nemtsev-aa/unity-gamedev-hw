using UnityEngine;
using System;

namespace AtomicFramework.RotationCompanent {

    [Serializable]
    public sealed class RotateConfig {
        [field: SerializeField] public RotationModes RotationMode { get; private set; } = RotationModes.Slerp;
        [field: SerializeField] public float RotateSpeed { get; private set; } = 5f;
        [field: SerializeField] public float SmoothTime { get; private set; } = 0.1f;
    }
}
