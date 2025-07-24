using MBT;
using UnityEngine;
using FarmingSystem;

namespace BehaviorTree.Brain {

    [AddComponentMenu("")]
    [MBTNode("Main Companents/" + nameof(FarmingOperationDecision))]
    public class FarmingOperationDecision : Composite {
        [SerializeField] private BotBrainDataReference _dataReference;
        [Space, SerializeField] private BoolReference _showDebugMessage;

        private BotBrainData _botBrainData;
        private ExecutionState _currentState;
        private NodeResult _currentResult;
        private bool _operationInProgress;

        private WorkOperationPriority Priority => _botBrainData.Priority.CurrentValue;
        private ResourceSpot CurrentSource => _botBrainData.NearestResourceSource.CurrentValue;
        private ResourceSpotSensor ResourceSensor => (ResourceSpotSensor)children[0];
        private Node FarmingNode => ResourceSensor.children[0];

        private ResourceLoot CurrentLoot => _botBrainData.NearestResourceLoot.CurrentValue;
        private LootSensor LootSensor => (LootSensor)children[1];
        private Node CollectionNode => LootSensor.children[0];

        private bool InventoryStatus => _botBrainData.InventoryStatus.CurrentValue;
        private InventorySensor InventorySensor => (InventorySensor)children[2];
        private Node DeliveringNode => InventorySensor.children[0];

        public override void OnEnter() {
            base.OnEnter();

            ResetState();

            _botBrainData = _dataReference.Value;

            if (_showDebugMessage.Value == true)
                Debug.Log($"<color=orange> {title} enter </color>");
        }

        public override NodeResult Execute() {

            if (Priority == WorkOperationPriority.Farming)
                return ExecuteFarmingFirst();
            else
                return ExecuteCollectingFirst();
        }

        private NodeResult ExecuteFarmingFirst() {

            if (_showDebugMessage.Value == true)
                Debug.Log($"<color=yellow> {title} execute FarmingFirst </color>");

            if (_currentState == ExecutionState.NotStarted ||
                _currentState == ExecutionState.Farming) {

                if (PreparationForFarming() == NodeResult.failure) {

                    if (PreparationForCollecting() == NodeResult.failure)
                        return NodeResult.failure;

                    _currentState = ExecutionState.Collecting;
                    return NodeResult.running;
                }

                return ExecuteFarming();
            }

            if (_currentState == ExecutionState.Collecting) {

                if (PreparationForCollecting() == NodeResult.failure)
                    return NodeResult.failure;

                return ExecuteCollecting();
            }

            if (_currentState == ExecutionState.Delivering) {

                if (PreparationForDelivering() == NodeResult.failure)
                    return NodeResult.failure;

                return ExecuteDelivering();
            }

            if (_showDebugMessage.Value == true)
                Debug.Log($"<color=green> {title} FarmingFirst succes </color>");

            return NodeResult.success;
        }

        private NodeResult ExecuteCollectingFirst() {

            if (_showDebugMessage.Value == true)
                Debug.Log($"<color=yellow> {title} execute CollectingFirst </color>");

            if (_currentState == ExecutionState.NotStarted ||
                _currentState == ExecutionState.Collecting) {

                if (PreparationForCollecting() == NodeResult.failure) {
                    _currentState = ExecutionState.Delivering;
                    return NodeResult.running;
                }

                return ExecuteCollecting();
            }

            if (_currentState == ExecutionState.Delivering) {

                if (PreparationForDelivering() == NodeResult.failure) {

                    if (CheckLootAvailable() == true) {
                        _currentState = ExecutionState.Collecting;
                        return NodeResult.running;
                    }

                    if (CheckResourceAvailable() == true) {
                        _currentState = ExecutionState.Farming;
                        return NodeResult.running;
                    }
                }

                _botBrainData.SetInventoryStatus(true);
                return ExecuteDelivering();
            }

            if (_currentState == ExecutionState.Farming) {

                if (PreparationForFarming() == NodeResult.failure)
                    return NodeResult.failure;

                return ExecuteFarming();
            }

            if (_showDebugMessage.Value == true)
                Debug.Log($"<color=green> {title} CollectingFirst succes </color>");

            return NodeResult.success;
        }

        private void ResetState() {
            _currentState = ExecutionState.NotStarted;
            _currentResult = NodeResult.failure;
            _operationInProgress = false;
        }

        #region Farming Operations

        private NodeResult PreparationForFarming() {

            if (_operationInProgress == false) {

                if (CheckResourceAvailable() == false)
                    return NodeResult.failure;

                FarmingNode.OnEnter();
                _operationInProgress = true;
            }

            return NodeResult.running;
        }

        private bool CheckResourceAvailable() {

            if (CurrentSource == null) {
                ResourceSensor.Task();

                return CurrentSource != null;
            }

            return true;
        }

        private NodeResult ExecuteFarming() {

            _currentResult = FarmingNode.Execute();

            if (_showDebugMessage.Value == true)
                Debug.Log($"<color=yellow> {FarmingNode.title} result {_currentResult.status} </color>");

            if (_currentResult == NodeResult.success) {
                FarmingNode.OnExit();
                _operationInProgress = false;
                _currentResult = NodeResult.running;
                _currentState = ExecutionState.Collecting;
            }

            return _currentResult;
        }

        #endregion

        #region Collecting Operations

        private NodeResult PreparationForCollecting() {

            if (_operationInProgress == false) {

                if (CheckLootAvailable() == false)
                    return NodeResult.failure;

                CollectionNode.OnEnter();
                _operationInProgress = true;
            }

            return NodeResult.running;
        }

        private bool CheckLootAvailable() {

            if (CurrentLoot == null) {
                LootSensor.Task();

                return CurrentLoot != null;
            }

            return true;
        }

        private NodeResult ExecuteCollecting() {
            _currentResult = CollectionNode.Execute();

            if (_showDebugMessage.Value == true)
                Debug.Log($"<color=yellow> {CollectionNode.title} result {_currentResult.status} </color>");

            if (_currentResult == NodeResult.success) {
                CollectionNode.OnExit();
                _operationInProgress = false;

                if (CheckLootAvailable() == true) {

                    if (CheckInventoryFillStatus() == true)
                        _currentState = ExecutionState.Delivering;
                    else
                        _currentState = ExecutionState.Collecting;

                }

                _currentResult = NodeResult.running;
            }

            return _currentResult;
        }

        #endregion

        #region Delivering Operations

        private NodeResult PreparationForDelivering() {

            if (_operationInProgress == false) {

                if (CheckInventoryFillStatus() == false)
                    return NodeResult.failure;

                DeliveringNode.OnEnter();
                _operationInProgress = true;
            }

            return NodeResult.running;
        }

        private bool CheckInventoryFillStatus() {
            InventorySensor.Task();

            return InventoryStatus;
        }

        private NodeResult ExecuteDelivering() {
            _currentResult = DeliveringNode.Execute();

            if (_showDebugMessage.Value == true)
                Debug.Log($"<color=yellow> {DeliveringNode.title} result {_currentResult.status} </color>");

            if (_currentResult == NodeResult.success) {
                DeliveringNode.OnExit();
                _operationInProgress = false;

                _currentResult = NodeResult.success;
                _currentState = ExecutionState.Completed;

                _botBrainData.SetFarmingZone(null);
            }

            return _currentResult;
        }

        #endregion

        #region Validation

        public override void AddChild(Node node) {

            if (children.Count >= 3) {
                Debug.LogError($"{nameof(FarmingOperationDecision)} can't have more than 3 children");
                return;
            }

            if (children.Count == 0 && !(node is ResourceSpotSensor)) {
                Debug.LogError($"First child of {nameof(FarmingOperationDecision)} must be of type ResourceSensor");
                return;
            }

            if (children.Count == 1 && !(node is LootSensor)) {
                Debug.LogError($"Second child of {nameof(FarmingOperationDecision)} must be of type LootSensor");
                return;
            }

            if (children.Count == 2 && !(node is InventorySensor)) {
                Debug.LogError($"Third child of {nameof(InventorySensor)} must be of type InventorySensor");
                return;
            }

            base.AddChild(node);
        }

        public override bool IsValid() {

            if (children.Count != 3) {
                Debug.LogError($"{nameof(FarmingOperationDecision)} must have exactly 3 children");
                return false;
            }

            if ((children[0] is ResourceSpotSensor) == false) {
                Debug.LogError($"First child of {nameof(FarmingOperationDecision)} must be ResourceSensor");
                return false;
            }

            if ((children[1] is LootSensor) == false) {
                Debug.LogError($"Second child of {nameof(FarmingOperationDecision)} must be LootSensor");
                return false;
            }

            if ((children[2] is InventorySensor) == false) {
                Debug.LogError($"Third child of {nameof(InventorySensor)} must be InventorySensor");
                return false;
            }

            if (children[0].children.Count == 0) {
                Debug.LogError($"ResourceSensor must have at least one child node");
                return false;
            }

            if (children[1].children.Count == 0) {
                Debug.LogError($"LootSensor must have at least one child node");
                return false;
            }

            if (children[2].children.Count == 0) {
                Debug.LogError($"HasFillInventory must have at least one child node");
                return false;
            }

            return base.IsValid();
        }

        public override void RemoveChild(Node node) {
            base.RemoveChild(node);
        }

        #endregion

        #region Enum

        private enum ExecutionState {
            NotStarted,
            Farming,
            Collecting,
            Delivering,
            Completed
        }

        #endregion
    }
}