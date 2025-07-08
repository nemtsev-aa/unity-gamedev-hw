using InventorySystem.Core;
using InventorySystem.ItemComponents;
using NUnit.Framework;

namespace UnitTests {

    [TestFixture]
    public class InventoryTests {
        private Inventory _inventory;
        private InventoryItem _stackableItem;
        private InventoryItem _nonStackableItem;

        [SetUp]
        public void Setup() {
            _inventory = new Inventory();

            _stackableItem = CreateTestItem("stack1", InventoryItemFlags.STACKABLE,
                new StackableItemComponent(5, 10));

            _nonStackableItem = CreateTestItem("item1", InventoryItemFlags.NONE);
        }

        [Test]
        public void TryAddItem_NonStackableItem_AddsNewItem() {
            // Act
            var result = _inventory.TryAddItem(_nonStackableItem);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _inventory.Items.Count);
            Assert.AreEqual(_nonStackableItem.ID, _inventory.Items[0].ID);
        }

        [Test]
        public void TryAddItem_StackableItem_AddsToExistingStack() {
            // Arrange
            _inventory.TryAddItem(_stackableItem.Clone());

            var sameItem = _stackableItem.Clone();

            // Act
            var result = _inventory.TryAddItem(sameItem);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, _inventory.Items.Count);
            Assert.IsTrue(_inventory.Items[0].TryGetComponent<StackableItemComponent>(out var component));
            Assert.AreEqual(6, component.Count);
        }

        [Test]
        public void TryRemoveItem_LastStackableItem_RemovesItem() {
            // Arrange
            _stackableItem = CreateTestItem("stack1", InventoryItemFlags.STACKABLE,
                new StackableItemComponent(1, 5));
            var item = _stackableItem.Clone();

            item.TryGetComponent<StackableItemComponent>(out var component);
            _inventory.TryAddItem(item);

            // Act
            var result = _inventory.TryRemoveItem(item);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _inventory.Items.Count);
        }

        [Test]
        public void TryRemoveItem_StackableItem_DecrementsCount() {
            // Arrange
            var item = _stackableItem.Clone();
            _inventory.TryAddItem(item);

            // Act
            var result = _inventory.TryRemoveItem(item);

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(_inventory.Items[0].TryGetComponent<StackableItemComponent>(out var component));
            Assert.AreEqual(4, component.Count);
        }

        private InventoryItem CreateTestItem(string id, InventoryItemFlags flags, params IItemComponent[] components) {
            return new InventoryItem {
                ID = id,
                Flags = flags,
                Metadata = new InventoryItemMetadata("Test Item", "Test Description", null),
                Components = components
            };
        }
    }
}
