using System;
using UnityEngine;

namespace ChestsSystem {

    [Serializable]
    public sealed class ChestData {
        [field: SerializeField] public ChestType Type { get; private set; }
        [field: SerializeField] public CooldownData Cooldown { get; private set; }

        public DateTime LastOpenTime { get; private set; }
        public bool IsReadyToOpen { get; private set; }

        public ChestData(ChestType type, TimeSpan timeSpan) {
            Type = type;
            Cooldown = new CooldownData(timeSpan);
        }
    }
}
