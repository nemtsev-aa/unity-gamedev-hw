using UnityEngine;
using InventorySystem.Core;
using InventorySystem.ItemComponents;

namespace InventorySystem.UI {

    public sealed class InventoryItemPresentationModel : IInventoryItemPresentationModel {
        public string Title => _item.Metadata.Name; 
        public string Description => _item.Metadata.Description;
        public Sprite Icon => _item.Metadata.Icon;

        private readonly InventoryItem _item;
        private readonly InventoryItemConsumer _consumeManager;

        public InventoryItemPresentationModel(InventoryItem item, InventoryItemConsumer consumeManager) {
            _item = item;
            _consumeManager = consumeManager;
        }

        public bool IsStackableItem() {
            return _item.Flags.HasFlag(InventoryItemFlags.STACKABLE);
        }

        public void GetStackInfo(out int current, out int size) {

            if (_item.TryGetComponent<StackableItemComponent>(out var component) == true) {
                current = component.Count;
                size = component.MaxCount;

                return;
            }

            current = 0;
            size = 0;
        }

        public bool IsConsumableItem() {
            return _item.Flags.HasFlag(InventoryItemFlags.CONSUMABLE);
        }

        public bool CanConsumeItem() {
            return _consumeManager.CanConsumeItem(_item);
        }

        public void OnConsumeClicked() {

            if (_consumeManager.CanConsumeItem(_item) == true)
                _consumeManager.ConsumeItem(_item);
        }
    }
}