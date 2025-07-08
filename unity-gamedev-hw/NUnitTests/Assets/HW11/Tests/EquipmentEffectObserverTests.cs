using Character;
using EquipmentSystem.Core;
using EquipmentSystem.Observers;
using InventorySystem.Core;
using InventorySystem.ItemComponents;
using NUnit.Framework;
using System.Collections.Generic;

namespace UnitTests {

    [TestFixture]
    public class EquipmentEffectObserverTests {
        private EquipmentEffectObserver _observer;
        private MockEquipmentService _equipmentService;
        private CharacterStatesProvider _statesProvider;
        private CharacterModel _characterModel;

        [SetUp]
        public void Setup() {
            _characterModel = new CharacterModel(new CharacterDefaultData {
                DafaultData = new List<CharacterDefaultStatData>
                {
                new CharacterDefaultStatData("Agility", 10)
            }
            });

            _statesProvider = new CharacterStatesProvider(_characterModel);
            _equipmentService = new MockEquipmentService();
            _observer = new EquipmentEffectObserver(_equipmentService, _statesProvider);
        }

        [Test]
        public void OnSlotEquiped_ItemWithEffect_AppliesEffect() {
            // Arrange
            var item = CreateTestItem("item1", InventoryItemFlags.EQUPPABLE,
                new EquippableItemComponent(EquipSlotTypes.Hands),
                new AgilityItemComponent(5));

            // Act
            _equipmentService.SimulateEquip(item);

            // Assert
            Assert.AreEqual(15, _characterModel.States[0].CurrentValue);
        }

        [Test]
        public void OnSlotUnequiped_ItemWithEffect_RemovesEffect() {
            // Arrange
            var item = CreateTestItem("item1", InventoryItemFlags.EQUPPABLE,
                new EquippableItemComponent(EquipSlotTypes.Hands),
                new AgilityItemComponent(5));

            _equipmentService.SimulateEquip(item);

            // Act
            _equipmentService.SimulateUnequip(item);

            // Assert
            Assert.AreEqual(10, _characterModel.States[0].CurrentValue);
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
