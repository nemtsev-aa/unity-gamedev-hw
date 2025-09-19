using MBT;
using UnityEngine;
using BehaviorTree.PlayerCompanents;

namespace BehaviorTree.Brain {

    [AddComponentMenu("")]
    [MBTNode("Main Companents/" + nameof(InventorySensor))]
    public class InventorySensor : Service {
        [SerializeField] private BotBrainDataReference _dataReference;
        [SerializeField] private PlayerReference _playerReference;

        [Space, SerializeField] private BoolReference _showDebugMessage;

        private BotBrainData _botBrainData;
        private InventoryCompanent _inventory;
        private bool _isEntered;

        public override void OnEnter() {

            if (_isEntered == true)
                return;

            _botBrainData = _dataReference.Value;
            _inventory = _playerReference.Value.Core.Inventory;
        }

        public override void Task() {

            if (_isEntered == false)
                OnEnter();

            _botBrainData.SetInventoryMaxAmount(_inventory.MaxAmount);
            _botBrainData.SetInventoryStatus(_inventory.IsFill);
            _botBrainData.UpdateCurrentInventoryAmount(_inventory.CurrentAmount);

            if (_showDebugMessage.Value == true)
                Debug.Log($"<color=yellow> {title}: task </color>");
        }
    }
}



