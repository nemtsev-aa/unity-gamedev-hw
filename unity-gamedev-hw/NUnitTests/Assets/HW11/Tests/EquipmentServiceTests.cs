using EquipmentSystem.Core;
using InventorySystem.Core;
using InventorySystem.ItemComponents;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests {
    [TestFixture]
    public class EquipmentServiceTests {
        private EquipmentService _equipmentService;
        private MockInventoryService _inventoryService;
        private InventoryItem _testWeapon;
        private InventoryItem _testArmor;
        private int _slotIndex;

        [SetUp]
        public void Setup() {

            var config = new EquipmentServiceConfig {
                SlotsConfig = new List<EquipmentSlotConfig>
                {
                new EquipmentSlotConfig(0, EquipSlotTypes.Hands),
                new EquipmentSlotConfig(1, EquipSlotTypes.Body)
            }
            };

            _inventoryService = new MockInventoryService();
            _equipmentService = new EquipmentService(config, _inventoryService);

            _testWeapon = CreateTestItem("weapon1", InventoryItemFlags.EQUPPABLE,
                new EquippableItemComponent(EquipSlotTypes.Hands),
                new AgilityItemComponent(5));

            _testArmor = CreateTestItem("armor1", InventoryItemFlags.EQUPPABLE,
                new EquippableItemComponent(EquipSlotTypes.Body),
                new AgilityItemComponent(2));
        }

        [Test]
        public void TryEquipItem_NonEquippableItem_Fails() {
            // Arrange
            _slotIndex = 0;
            var nonEquippableItem = CreateTestItem("item1", InventoryItemFlags.NONE);
            _inventoryService.Inventory.TryAddItem(nonEquippableItem);

            // Act
            var result = _equipmentService.TryEquipItem(nonEquippableItem, _slotIndex);

            // Assert
            Assert.IsFalse(result);
            Assert.IsNull(_equipmentService.GetEquippedItem(_slotIndex));
        }

        [Test]
        public void TryEquipItem_ValidArmor_EquipsToBodySlot() {
            // Arrange
            _slotIndex = 1;
            _inventoryService.Inventory.TryAddItem(_testArmor);

            // Act
            var result = _equipmentService.TryEquipItem(_testArmor, _slotIndex);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(_testArmor, _equipmentService.GetEquippedItem(_slotIndex));
            Assert.AreEqual(EquipSlotTypes.Body, _equipmentService.GetSlotById(_slotIndex).Type);
            Assert.IsFalse(_inventoryService.Inventory.Items.Contains(_testArmor));
        }

        [Test]
        public void TryEquipItem_ValidItem_EquipsSuccessfully() {
            // Arrange
            _slotIndex = 0;
            _inventoryService.Inventory.TryAddItem(_testWeapon);

            // Act
            var result = _equipmentService.TryEquipItem(_testWeapon, _slotIndex);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(_testWeapon, _equipmentService.GetEquippedItem(_slotIndex));
            Assert.IsFalse(_inventoryService.Inventory.Items.Contains(_testWeapon));
        }

        [Test]
        public void TryEquipItem_ValidWeapon_EquipsToHandsSlot() {
            // Arrange
            _slotIndex = 0;
            _inventoryService.Inventory.TryAddItem(_testWeapon);

            // Act
            var result = _equipmentService.TryEquipItem(_testWeapon, _slotIndex);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(_testWeapon, _equipmentService.GetEquippedItem(_slotIndex));
            Assert.AreEqual(EquipSlotTypes.Hands, _equipmentService.GetSlotById(_slotIndex).Type);
            Assert.IsFalse(_inventoryService.Inventory.Items.Contains(_testWeapon));
        }

        [Test]
        public void TryEquipItem_WrongSlotType_Fails() {
            // Arrange
            _slotIndex = 1;
            _inventoryService.Inventory.TryAddItem(_testWeapon);

            // Act
            var result = _equipmentService.TryEquipItem(_testWeapon, _slotIndex); // Пытаемся экипировать оружие в слот для брони

            // Assert
            Assert.IsFalse(result);
            Assert.IsNull(_equipmentService.GetEquippedItem(_slotIndex));
        }

        [Test]
        public void TryUnequipItem_EquippedItem_UnequipsSuccessfully() {
            // Arrange
            _slotIndex = 0;
            _inventoryService.Inventory.TryAddItem(_testWeapon);
            _equipmentService.TryEquipItem(_testWeapon, _slotIndex);

            // Act
            var result = _equipmentService.TryUnequipItem(_slotIndex);

            // Assert
            Assert.IsTrue(result);
            Assert.IsNull(_equipmentService.GetEquippedItem(_slotIndex));
            Assert.IsTrue(_inventoryService.Inventory.Items.Contains(_testWeapon));
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
