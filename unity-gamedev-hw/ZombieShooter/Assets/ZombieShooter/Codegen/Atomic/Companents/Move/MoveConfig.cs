using UnityEngine;
using System;

namespace AtomicFramework.MoveCompanent {

    [Serializable]
    public sealed class MoveConfig {
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public Vector3 MoveDirection { get; private set; }
        [field: SerializeField] public bool IsMoving { get; private set; }
    }
}
