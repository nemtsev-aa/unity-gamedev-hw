using System;
using UnityEngine;
using Character;
using EquipmentSystem.Core;
using InventorySystem.Core;
using InventorySystem.ItemComponents;

namespace EquipmentSystem.Observers {

    public sealed class EquipmentEffectObserver {
        private readonly IEquipmentService _equipmentService;
        private readonly CharacterStatesProvider _characterStatesProvider;

        public EquipmentEffectObserver(IEquipmentService equipmentService,
                                       CharacterStatesProvider characterStatesProvider) {

            _equipmentService = equipmentService;
            _characterStatesProvider = characterStatesProvider;

            _equipmentService.SlotEquiped += OnSlotEquiped;
            _equipmentService.SlotUnequiped += OnSlotUnequiped;
        }

        private void OnSlotEquiped(EquipmentSlot slot, InventoryItem item) {

            if (item == null)
                return;

            if (item.Flags.HasFlag(InventoryItemFlags.EQUPPABLE) == false)
                return;

            if (item.TryGetComponent<IPassiveEffectItemComponent>(out var component) == true) {

                if (_characterStatesProvider.TryGetStateByName(component.Type, out var stat) == true) {
                    var newValue = stat.Value.CurrentValue + component.Value;
                    stat.ChangeValue(newValue);

                    Debug.Log($"<color=green>Add {component.Type}: +{component.Value}</color>");
                    return;
                }

                throw new ArgumentException($"Invalid Stat Name: {component.Type}");
            }
        }

        private void OnSlotUnequiped(EquipmentSlot slot, InventoryItem item) {

            if (item == null)
                return;

            if (item.Flags.HasFlag(InventoryItemFlags.EQUPPABLE) == false)
                return;

            if (item.TryGetComponent<IPassiveEffectItemComponent>(out var component) == true) {

                if (_characterStatesProvider.TryGetStateByName(component.Type, out var stat) == true) {
                    var newValue = stat.Value.CurrentValue - component.Value;
                    stat.ChangeValue(newValue);

                    Debug.Log($"<color=red>Remove {component.Type}: -{component.Value}</color>");
                    return;
                }

                throw new ArgumentException($"Invalid Stat Name: {component.Type}");
            }
        }
    }
}
