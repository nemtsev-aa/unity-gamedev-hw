using System;
using UnityEngine;

namespace ChestsSystem {

    [Serializable]
    public sealed class RewardResourceData {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public string Amount { get; private set; }
    }
}
