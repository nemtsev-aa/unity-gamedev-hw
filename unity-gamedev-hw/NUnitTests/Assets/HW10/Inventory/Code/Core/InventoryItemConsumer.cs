using System;

namespace InventorySystem.Core {

    public class InventoryItemConsumer {

        public event Action<InventoryItem> ItemConsumed;

        private IInventory _inventory;

        public InventoryItemConsumer(InventoryService service) {
            _inventory = service.Inventory;
        }

        public void ConsumeItem(InventoryItem item) {

            if (CanConsumeItem(item) == false)
                throw new Exception($"Can not consume item {item.ID}!");

            _inventory.TryRemoveItem(item);
            ItemConsumed?.Invoke(item);
        }

        public bool CanConsumeItem(InventoryItem prototypeItem) {

            if (_inventory.TryFindItem(prototypeItem.ID, out var item) == false)
                return false;

            if (item.Flags.HasFlag(InventoryItemFlags.CONSUMABLE) == false)
                return false;

            return true;
        }
    }
}

