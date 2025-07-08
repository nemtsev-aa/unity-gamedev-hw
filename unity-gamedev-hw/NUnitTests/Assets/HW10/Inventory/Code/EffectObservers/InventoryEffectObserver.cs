using UnityEngine;
using Character;
using InventorySystem.Core;
using InventorySystem.ItemComponents;


namespace InventorySystem.EffectObservers {

    public class InventoryEffectObserver : IInventoryEffectObserver {
        private readonly Inventory _inventory;
        private readonly CharacterStatesProvider _characterStatesProvider;

        public InventoryEffectObserver(Inventory inventory,
                                       CharacterStatesProvider characterStatesProvider) {
            _inventory = inventory;
            _characterStatesProvider = characterStatesProvider;

            _inventory.ItemAdded += OnItemAdded;
            _inventory.ItemRemoved += OnItemRemoved;
        }

        public void OnItemAdded(InventoryItem item) {

            if (item.Flags.HasFlag(InventoryItemFlags.EFFECTIBLE) == false)
                return;

            if (item.TryGetComponent<IPassiveEffectItemComponent>(out var component) == true) {
                _characterStatesProvider.TryGetStateByName(component.Type, out var stat);

                if (stat != null) {
                    var newValue = stat.Value.CurrentValue + component.Value;
                    stat.ChangeValue(newValue);

                    Debug.Log($"<color=green>Add [{component.Type} Effect]: +{component.Value}</color>");
                }
            }
        }

        public void OnItemRemoved(InventoryItem item) {

            if (item.Flags.HasFlag(InventoryItemFlags.EFFECTIBLE) == false)
                return;

            if (item.TryGetComponent<IPassiveEffectItemComponent>(out var component) == true) {

                _characterStatesProvider.TryGetStateByName(component.Type, out var stat);

                if (stat != null) {
                    var newValue = stat.Value.CurrentValue - component.Value;
                    stat.ChangeValue(newValue);

                    Debug.Log($"<color=red>Remove [{component.Type} Effect]: -{component.Value}</color>");
                }
            }
        }
    }
}

