using System;
using UnityEngine;

namespace InteractionService {

    [Serializable]
    public class InteractionConfig {
        [field: SerializeField] public InteractionTypes InteractionType { get; private set; }
    }
}

