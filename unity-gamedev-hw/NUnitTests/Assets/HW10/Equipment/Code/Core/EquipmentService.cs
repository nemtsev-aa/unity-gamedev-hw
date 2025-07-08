using InventorySystem.Core;
using InventorySystem.ItemComponents;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EquipmentSystem.Core {

    public sealed class EquipmentService : IEquipmentService {
        public event Action<EquipmentSlot, InventoryItem> SlotEquiped;
        public event Action<EquipmentSlot, InventoryItem> SlotUnequiped;

        private readonly IInventory _inventory;
        private readonly List<EquipmentSlot> _slots;

        public IReadOnlyList<EquipmentSlot> Slots => _slots.AsReadOnly();

        public EquipmentService(EquipmentServiceConfig config, IInventoryService inventoryService) {
            _inventory = inventoryService.Inventory;
            _slots = CreateSlots(config.SlotsConfig);
        }

        public bool TryEquipItem(InventoryItem item, int slotIndex) {
            if (item.TryGetComponent<EquippableItemComponent>(out var equipComponent) == false)
                return false;

            var targetSlot = FindValidSlot(equipComponent.Type, slotIndex);
            if (targetSlot == null)
                return false;

            if (TryUnequipItem(slotIndex) == false)
                return false;

            return ProcessEquip(item, targetSlot);
        }

        public bool TryUnequipItem(int slotIndex) {

            var currentlyEquipped = GetEquippedItem(slotIndex);

            if (currentlyEquipped != null) {

                if (_inventory.TryAddItem(currentlyEquipped) == false)
                    return false;

                var currentlySlot = GetSlotById(slotIndex);
                currentlySlot.SetItem(null);
                SlotUnequiped?.Invoke(currentlySlot, currentlyEquipped);
            }

            return true;
        }

        public InventoryItem GetEquippedItem(int slotIndex) {
            return _slots.FirstOrDefault(s => s.Index == slotIndex)?.Item;
        }

        public IEnumerable<InventoryItem> GetEquippedItems(EquipSlotTypes slotType) {
            return _slots
                .Where(s => s.Type == slotType)
                .Select(s => s.Item)
                .Where(item => item != null);
        }

        public EquipmentSlot GetSlotById(int index) {
            return _slots.FirstOrDefault(s => s.Index == index);
        }

        private List<EquipmentSlot> CreateSlots(List<EquipmentSlotConfig> configs) {
            return configs.Select(c => new EquipmentSlot(c.Index, c.Type)).ToList();
        }

        private bool ProcessEquip(InventoryItem item, EquipmentSlot slot) {

            if (_inventory.TryRemoveItem(item) == false)
                return false;

            slot.SetItem(item);
            SlotEquiped?.Invoke(slot, item);

            return true;
        }

        private EquipmentSlot FindValidSlot(EquipSlotTypes type, int slotIndex) {
            return _slots.FirstOrDefault(s =>
                s.Type == type &&
                (slotIndex == -1 || s.Index == slotIndex));
        }
    }
}
