using System;
using System.Collections.Generic;
using UnityEngine;

namespace ChestsSystem {

    [Serializable]
    public sealed class ChestSystemConfig {
        [field: SerializeField] public ChestVisualProvider ChestVisualProvider { get; private set; }
        [field: SerializeField] public List<ChestData> ChestDatas { get; private set; }
        [field: SerializeField] public List<ChestReward> ChestRewards { get; private set; }
    }
}
