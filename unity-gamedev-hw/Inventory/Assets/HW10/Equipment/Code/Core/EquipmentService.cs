using System;
using System.Linq;
using System.Collections.Generic;
using InventorySystem.Core;
using InventorySystem.ItemComponents;

namespace EquipmentSystem.Core {

    public sealed class EquipmentService : IEquipmentService {
        public event Action<EquipmentSlot, InventoryItem> SlotEquiped;
        public event Action<EquipmentSlot, InventoryItem> SlotUnequiped;

        private readonly IInventoryService _inventoryService;
        private readonly List<EquipmentSlot> _slots;

        public IReadOnlyList<EquipmentSlot> Slots => _slots.AsReadOnly();

        public EquipmentService(EquipmentServiceConfig config, IInventoryService inventoryService) {
            _inventoryService = inventoryService;
            _slots = CreateSlots(config.SlotsConfig);
        }

        public bool TryEquipItem(InventoryItem item, int slotIndex) {
            if (item.TryGetComponent<EquippableItemComponent>(out var equipComponent) == false)
                return false;

            var targetSlot = FindValidSlot(equipComponent.Type, slotIndex);
            if (targetSlot == null)
                return false;

            return ProcessEquip(item, targetSlot);
        }

        private EquipmentSlot FindValidSlot(EquipSlotTypes type, int slotIndex) {
            return _slots.FirstOrDefault(s =>
                s.Type == type &&
                (slotIndex == -1 || s.Index == slotIndex));
        }

        private bool ProcessEquip(InventoryItem item, EquipmentSlot slot) {
            var currentlyEquipped = GetEquippedItem(slot.Index);

            if (currentlyEquipped != null) {
                
                if (TryUnequipItem(slot) == false) 
                    return false;
                
                _inventoryService.Inventory.TryAddItem(currentlyEquipped);
            }

            slot.SetItem(item);
            _inventoryService.Inventory.TryRemoveItem(item);
            SlotEquiped?.Invoke(slot, item);

            return true;
        }

        public bool TryUnequipItem(EquipmentSlot slot) {
            if (slot?.Item == null)
                return false;

            SlotUnequiped?.Invoke(slot, slot.Item);
            slot.SetItem(null);
            
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
    }
}
