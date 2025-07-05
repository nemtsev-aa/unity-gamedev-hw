namespace InventorySystem.Core {

    public sealed class InventoryService : IInventoryService {
        private readonly Inventory _inventory;

        public InventoryService(Inventory inventory) {
            _inventory = inventory;
        }

        public IInventory Inventory => _inventory;
    }
}

