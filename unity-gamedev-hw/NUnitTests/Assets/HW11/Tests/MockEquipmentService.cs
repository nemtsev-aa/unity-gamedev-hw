using System;
using EquipmentSystem.Core;
using InventorySystem.Core;
using System.Collections.Generic;

namespace UnitTests {
    public class MockEquipmentService : IEquipmentService {
        public event Action<EquipmentSlot, InventoryItem> SlotEquiped;
        public event Action<EquipmentSlot, InventoryItem> SlotUnequiped;

        public InventoryItem GetEquippedItem(int slotIndex) {
            throw new NotImplementedException();
        }

        public IEnumerable<InventoryItem> GetEquippedItems(EquipSlotTypes slotType) {
            throw new NotImplementedException();
        }

        public EquipmentSlot GetSlotById(int index) {
            throw new NotImplementedException();
        }

        public void SimulateEquip(InventoryItem item) {
            SlotEquiped?.Invoke(new EquipmentSlot(0, EquipSlotTypes.Hands), item);
        }

        public void SimulateUnequip(InventoryItem item) {
            SlotUnequiped?.Invoke(new EquipmentSlot(0, EquipSlotTypes.Hands), item);
        }

        public bool TryEquipItem(InventoryItem item, int slotIndex) {
            throw new NotImplementedException();
        }

        public bool TryUnequipItem(int slotIndex) {
            throw new NotImplementedException();
        }
    }
}
