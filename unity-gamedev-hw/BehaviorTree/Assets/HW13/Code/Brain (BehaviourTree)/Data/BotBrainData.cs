using R3;
using System;
using UnityEngine;
using FarmingSystem;
using System.Collections.Generic;

namespace BehaviorTree.Brain {

    [Serializable]
    public sealed class BotBrainData {
        // Core
        public Transform Root { get; private set; }
        public ReadOnlyReactiveProperty<BotStates> BotState => _botState;

        // Moving
        public ReadOnlyReactiveProperty<Transform> MoveTarget => _moveTarget;
        public ReadOnlyReactiveProperty<float> DistanceToTarget => _distanceToTarget;

        // Patrol
        public ReadOnlyReactiveProperty<Transform> PartolPoint => _partolPoint;
        public IReadOnlyList<Transform> Waypoints => _waypoints;

        // Farming
        public ReadOnlyReactiveProperty<WorkOperationPriority> Priority => _priority;
        public Transform DeliveryTarget { get; private set; }
        public IReadOnlyList<FellingZone> FellingZones => _fellingZones;
        public ReadOnlyReactiveProperty<FellingZone> NearestFarmingZone => _nearestFarmingZone;
        public ReadOnlyReactiveProperty<ResourceSpot> NearestResourceSource => _nearestResourceSource;
        public ReadOnlyReactiveProperty<ResourceLoot> NearestResourceLoot => _nearestResourceLoot;
        public ReadOnlyReactiveProperty<bool> InventoryStatus => _inventoryStatus;
        public ReadOnlyReactiveProperty<int> MaxInventoryAmount => _inventoryMaxAmount;
        public ReadOnlyReactiveProperty<int> CurrentInventoryAmount => _currentInventoryAmount;

        private ReactiveProperty<BotStates> _botState = new(BotStates.Idle);
        private ReactiveProperty<WorkOperationPriority> _priority = new();
        private ReactiveProperty<Transform> _moveTarget = new();
        private ReactiveProperty<float> _distanceToTarget = new(0);
        private ReactiveProperty<Transform> _partolPoint = new();
        private ReactiveProperty<FellingZone> _nearestFarmingZone = new();
        private ReactiveProperty<ResourceSpot> _nearestResourceSource = new();
        private ReactiveProperty<ResourceLoot> _nearestResourceLoot = new();
        private ReactiveProperty<bool> _inventoryStatus = new();
        private ReactiveProperty<int> _currentInventoryAmount = new();
        private List<Transform> _waypoints = new();
        private List<FellingZone> _fellingZones = new();
        private ReactiveProperty<int> _inventoryMaxAmount = new();

        public BotBrainData(BotBrainDataConfig config) {
            Root = config.Root;
            DeliveryTarget = config.DeliveryTarget;
            _priority = new ReactiveProperty<WorkOperationPriority>(config.Priority);
            _waypoints = config.Waypoints.Points;
            _fellingZones = config.FellingZones.Zones;
        }

        public void SwitchBotState(BotStates state) {

            if (_botState.Value != state)
                _botState.Value = state;

            //Debug.Log($"BotBrainData: {_botState.Value}");
        }

        public void SwitchWorkOperationPriority(WorkOperationPriority priority) {
            _priority.Value = priority;
        }

        public void ResetMoveTarget() {

            //Debug.Log($"BotBrainData: ResetMoveTarget!");
            _moveTarget.Value = null;
        }

        public void SetFarmingZone(FellingZone zone) {

            if (_nearestFarmingZone.Value == zone) {
                //Debug.Log($"Re-assignment FarmingZone");
                return;
            }

            _nearestFarmingZone.Value = zone;

            if (zone == null)
                ResetMoveTarget();
            else
                SetMoveTarget(_nearestFarmingZone.Value.transform);
        }

        public void SetResourceSource(ResourceSpot source) {

            if (_nearestResourceSource.Value == source) {
                //Debug.Log($"Re-assignment ResourceSource");
                return;
            }

            _nearestResourceSource.Value = source;

            if (source == null)
                ResetMoveTarget();
            else
                SetMoveTarget(_nearestResourceSource.Value.transform);
        }

        public void SetResourceLoot(ResourceLoot loot) {

            if (_nearestResourceLoot.Value == loot) {
                //Debug.Log($"Re-assignment ResourceLoot");
                return;
            }

            _nearestResourceLoot.Value = loot;

            if (loot == null)
                ResetMoveTarget();
            else
                SetMoveTarget(_nearestResourceLoot.Value.transform);
        }

        public void SetInventoryMaxAmount(int maxAmount) {
            _inventoryMaxAmount.Value = maxAmount;
        }

        public void SetInventoryStatus(bool status) {
            _inventoryStatus.Value = status;

            if (status == true)
                SetMoveTarget(DeliveryTarget);
        }

        public void UpdateCurrentInventoryAmount(int amount) {
            _currentInventoryAmount.Value = amount;

            if (_currentInventoryAmount.Value == 0)
                SetInventoryStatus(false);
        }

        public void SetCurrentPatrolPoint(Transform transform) {
            _partolPoint.Value = transform;

            if (transform == null)
                ResetMoveTarget();
            else
                SetMoveTarget(_partolPoint.Value);
        }

        public void SetDistanceToTarget(float distanceToTarget) {
            _distanceToTarget.Value = distanceToTarget;
        }

        private void SetMoveTarget(Transform transform) {

            if (_moveTarget.Value == transform) {
                //Debug.Log($"Re-assignment MoveTarget");
                return;
            }

            _moveTarget.Value = transform;
            CheckCurrentState();
        }

        private void CheckCurrentState() {

            Transform currentMoveTarget = _moveTarget.Value;

            if (currentMoveTarget == null)
                return;

            if (_nearestFarmingZone.CurrentValue != null && currentMoveTarget == _nearestFarmingZone.CurrentValue.transform) {
                SwitchBotState(BotStates.MoveToFarmingZone);
                return;
            }

            if (_nearestResourceSource.CurrentValue != null && currentMoveTarget == _nearestResourceSource.CurrentValue.transform) {
                SwitchBotState(BotStates.MoveToResourceSource);
                return;
            }

            if (_nearestResourceLoot.CurrentValue != null && currentMoveTarget == _nearestResourceLoot.CurrentValue.transform) {
                SwitchBotState(BotStates.MoveToResourceLoot);
                return;
            }

            if (DeliveryTarget != null && currentMoveTarget == DeliveryTarget) {
                SwitchBotState(BotStates.MoveToDeliveryTarget);
                return;
            }

            if (_partolPoint.CurrentValue != null && currentMoveTarget == _partolPoint.CurrentValue) {
                SwitchBotState(BotStates.Patrol);
                return;
            }
        }
    }
}



