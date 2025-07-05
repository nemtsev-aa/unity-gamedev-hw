using System;
using UnityEngine;
using EquipmentSystem.Core;

namespace InventorySystem.ItemComponents {

    [Serializable]
    public sealed class EquippableItemComponent : IItemComponent {

        [field: SerializeField] public EquipSlotTypes Type { get; private set; }
        [field: SerializeField, Min(1)] public int RequiredSlots { get; private set; } = 1; // Для двуручного оружия

        public EquippableItemComponent(EquipSlotTypes type, int requiredSlots = 1) {
            Type = type;
            RequiredSlots = requiredSlots;
        }

        public IItemComponent Clone() {
            return new EquippableItemComponent(Type, RequiredSlots);
        }
    }
}

