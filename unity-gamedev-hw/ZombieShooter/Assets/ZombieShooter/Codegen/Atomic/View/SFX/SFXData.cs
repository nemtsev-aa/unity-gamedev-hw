using System;
using UnityEngine;

namespace AtomicFramework.View.SFX {

    [Serializable]
    public sealed class SFXData {
        [field: SerializeField] public int ActionId { get; private set; }
        [field: SerializeField] public AudioClip Effect { get; private set; }
    }
}
