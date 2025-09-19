using System;
using UnityEngine;

namespace HintPlayerControlService {
    [Serializable]
    public sealed class HintPlayerControlConfig {
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}