using System;
using Sirenix.OdinInspector;

namespace InventorySystem.Core {

    [Serializable]
    public sealed class InventoryHelper  {
        private readonly IInventory _inventory;
        private readonly InventoryItemConsumer _inventoryItemConsumer;
        private readonly InventoryItemConfigProvider _configProvider;

        public InventoryHelper(IInventoryService service,
                              InventoryItemConsumer consumer,
                              InventoryItemConfigProvider configProvider) {

            _inventory = service.Inventory;
            _inventoryItemConsumer = consumer;
            _configProvider = configProvider;
        }

        public void FillInventory() {

            var configsCount = _configProvider.GetConfigsListCount();

            if (configsCount <= 0)
                throw new ArgumentNullException($"InventoryItemConfig list is emply!");

            for (int i = 0; i < configsCount; i++) {
               
                if (_configProvider.TryGetItemByIndex(i, out var inventoryItem) == true) 
                    _inventory.TryAddItem(inventoryItem);
            }
        }

        [Button]
        private void AddItem(InventoryItemConfig itemConfig) {

            if (itemConfig == null)
                return;

            var item = itemConfig.Clone();
            _inventory.TryAddItem(item);
        }

        [Button]
        private void RemoveItem(InventoryItemConfig itemConfig) {

            if (itemConfig == null)
                return;

            var item = itemConfig.Clone();
            _inventory.TryRemoveItem(item);
        }

        [Button]
        private void ConsumeItem(InventoryItemConfig itemConfig) {

            if (itemConfig == null)
                return;

            var item = itemConfig.Clone();
            _inventoryItemConsumer.ConsumeItem(item);
        }
    }
}

