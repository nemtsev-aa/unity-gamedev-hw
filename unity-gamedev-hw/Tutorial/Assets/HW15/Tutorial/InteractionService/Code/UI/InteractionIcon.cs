using System;
using UnityEngine;

namespace InteractionService {

    [Serializable]
    public sealed class InteractionIcon {
        [field: SerializeField] public InteractionTypes Type { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}
