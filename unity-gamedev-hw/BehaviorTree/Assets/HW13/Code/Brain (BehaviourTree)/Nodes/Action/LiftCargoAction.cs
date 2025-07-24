using MBT;
using UnityEngine;
using FarmingSystem;
using BehaviorTree.PlayerCompanents;
using BehaviorTree.PlayerCoreSubsystem;

namespace BehaviorTree.Brain {

    [AddComponentMenu("")]
    [MBTNode("Main Companents/" + nameof(LiftCargoAction))]
    public class LiftCargoAction : Leaf {
        [SerializeField] private BotBrainDataReference _dataReference;
        [SerializeField] private PlayerReference _playerReference;
        [Space, SerializeField] private BoolReference _showDebugMessage;

        private BotBrainData _botBrainData;
        private PlayerCore _playerCore;

        private CollectorCompanent _collector;
        private InventoryCompanent _inventory;

        private Vector3 BotPosition => _playerCore.transform.position;

        public override void OnEnter() {
            _botBrainData = _dataReference.Value;
            _playerCore = _playerReference.Value.Core;
            _collector = _playerCore.Collector;
            _inventory = _playerCore.Inventory;

            _collector.Activate(true);
            _inventory.Activate(true);

            _botBrainData.SwitchBotState(BotStates.LiftResourceLoot);

            if (_showDebugMessage.Value == true)
                Debug.Log($"<color=orange> {title}: enter </color>");
        }

        public override NodeResult Execute() {

            if (_showDebugMessage.Value == true)
                Debug.Log($"<color=yellow> {title}: execute </color>");

            var resourceLoot = _botBrainData.NearestResourceLoot.CurrentValue;

            if (resourceLoot == null || resourceLoot.gameObject.activeInHierarchy == false)
                return NodeResult.failure;

            if (_inventory.IsFill == true || _inventory.VacantPlace < resourceLoot.Amount)
                return NodeResult.failure;

            float distanceToLoot = Vector3.Distance(BotPosition, resourceLoot.transform.position);

            if (distanceToLoot > _collector.DistanceToCollect)
                return NodeResult.running;

            if (_collector.TryCollect(resourceLoot) == false)
                return NodeResult.running;

            AddItemToInventory(resourceLoot);
            ClearReferences();

            if (_showDebugMessage.Value == true)
                Debug.Log($"<color=green> {title}: success </color>");

            return NodeResult.success;
        }

        private void AddItemToInventory(Loot loot) {
            var item = new InventoryItem(loot.Id, loot.Amount);

            if (_inventory.TryAddItem(item) == true)
                _botBrainData.UpdateCurrentInventoryAmount(_inventory.CurrentAmount);
        }

        private void ClearReferences() {
            _botBrainData.SetResourceLoot(null);
        }

        public override void OnExit() {
            _collector.Activate(false);
            _inventory.Activate(false);

            if (_showDebugMessage.Value == true)
                Debug.Log($"<color=red> {title}: exit </color>");
        }
    }
}
