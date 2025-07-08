using InventorySystem.Core;

namespace UnitTests {

    public class MockInventoryService : IInventoryService {
        public Inventory Inventory { get; } = new Inventory();

        IInventory IInventoryService.Inventory => Inventory;
    }
}