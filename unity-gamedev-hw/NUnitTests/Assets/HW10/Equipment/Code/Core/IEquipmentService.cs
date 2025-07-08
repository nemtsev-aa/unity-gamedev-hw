using System;
using System.Collections.Generic;
using InventorySystem.Core;

namespace EquipmentSystem.Core {

    public interface IEquipmentService {

        event Action<EquipmentSlot, InventoryItem> SlotEquiped;
        event Action<EquipmentSlot, InventoryItem> SlotUnequiped;

        bool TryEquipItem(InventoryItem item, int slotIndex);
        bool TryUnequipItem(int slotIndex);
        InventoryItem GetEquippedItem(int slotIndex);
        IEnumerable<InventoryItem> GetEquippedItems(EquipSlotTypes slotType);
        EquipmentSlot GetSlotById(int index);
    }
}
