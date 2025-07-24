using MBT;
using Zenject;
using Elementary;
using UnityEngine;
using Conveyors.Entity;
using BehaviorTree.PlayerCompanents;

namespace BehaviorTree.Brain {

    [AddComponentMenu("")]
    [MBTNode("Main Companents/" + nameof(DropLootAction))]
    public class DropLootAction : Leaf {
        [SerializeField] private BotBrainDataReference _dataReference;
        [SerializeField] private PlayerReference _playerReference;
        [Space, SerializeField] private BoolReference _showDebugMessage;

        private IntVariableLimited _loadStorage;
        private BotBrainData _botBrainData;
        private InventoryCompanent _inventory;
        private string _conveyorInputResourceType;

        [Inject]
        public void Construct(ConveyorModel conveyor) {
            _loadStorage = conveyor.Core.LoadStorage;
            _conveyorInputResourceType = conveyor.Config.InputResourceType.ToString();
        }

        public override void OnEnter() {
            _botBrainData = _dataReference.Value; ;
            _inventory = _playerReference.Value.Core.Inventory;

            _inventory.Activate(true);

            if (_showDebugMessage.Value == true)
                Debug.Log($"<color=orange> {title}: enter </color>");
        }

        public override NodeResult Execute() {

            if (_showDebugMessage.Value == true)
                Debug.Log($"<color=yellow> {title}: execute </color>");

            _botBrainData.SwitchBotState(BotStates.DropResourceLoot);

            var currentQuantity = _botBrainData.CurrentInventoryAmount.CurrentValue;

            for (int i = 0; i < currentQuantity; i++) {
                _loadStorage.Current += 1;

                var item = new InventoryItem(_conveyorInputResourceType, 1);
                RemoveItemToInventory(item);
            }

            if (_botBrainData.InventoryStatus.CurrentValue == false) {

                if (_showDebugMessage.Value == true)
                    Debug.Log($"<color=green> {title}: success </color>");

                return NodeResult.success;
            }

            return NodeResult.running;
        }

        public override void OnExit() {
            _inventory.Activate(false);

            if (_showDebugMessage.Value == true)
                Debug.Log($"<color=red> {title}: exit </color>");
        }

        private void RemoveItemToInventory(InventoryItem item) {

            if (_inventory.TryRemoveItem(item) == true) {
                int currentValue = _botBrainData.CurrentInventoryAmount.CurrentValue - item.Amount;
                _botBrainData.UpdateCurrentInventoryAmount(currentValue);
            }
        }
    }
}
