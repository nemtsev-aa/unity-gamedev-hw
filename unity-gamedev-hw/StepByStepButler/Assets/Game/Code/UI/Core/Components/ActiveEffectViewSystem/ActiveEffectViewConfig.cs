using System;
using UnityEngine;

namespace UI.Components.ActiveEffectViewSystem {

    [Serializable]
    public sealed class ActiveEffectViewConfig {
        [field: SerializeField] public ActiveEffectType Type { get; private set; }
        [field: SerializeField] public ActiveEffectView Prefab { get; private set; }
    }
}
