using System;
using InventorySystem.Core;
using InventorySystem.ItemComponents;

namespace EquipmentSystem.Core {

    [Serializable]
    public sealed class EquipmentSlot {
        public EquipmentSlot(int index, EquipSlotTypes type) {
            Index = index;
            Type = type;
        }

        public int Index { get; private set; } 
        public EquipSlotTypes Type { get; private set; }
        public InventoryItem Item { get; private set; }

        public void SetItem(InventoryItem item) {
            Item = item;
        }
    }
}
