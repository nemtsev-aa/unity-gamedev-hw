using System;
using System.Collections.Generic;
using UnityEngine;

namespace ChestsSystem {

    [Serializable]
    public sealed class ChestReward {
        [field: SerializeField] public string RewardId { get; private set; }
        [field: SerializeField] public int SoftCurrency { get; private set; }
        [field: SerializeField] public List<RewardResourceData> Resources { get; private set; }

        public ChestReward(string rewardId, int softCurrency, List<RewardResourceData> resources) {
            RewardId = rewardId;
            SoftCurrency = softCurrency;
            Resources = resources;
        }
    }
}
