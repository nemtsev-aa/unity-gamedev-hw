using System;
using UnityEngine;

namespace AtomicFramework.View.VFX {

    [Serializable]
    public sealed class VFXData {
        [field: SerializeField] public int Id { get; private set; }
        [field: SerializeField] public GameObject Effect { get; private set; }
    }
}
