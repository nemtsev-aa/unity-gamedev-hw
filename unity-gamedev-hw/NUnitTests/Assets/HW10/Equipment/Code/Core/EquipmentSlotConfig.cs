using System;
using UnityEngine;

namespace EquipmentSystem.Core {

    [Serializable]
    public sealed class EquipmentSlotConfig {
        public EquipmentSlotConfig(int index, EquipSlotTypes type) {
            Index = index;
            Type = type;
        }

        [field: SerializeField] public int Index { get; private set; }
        [field: SerializeField] public EquipSlotTypes Type { get; private set; }
    }
}
