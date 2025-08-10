using MBT;
using UnityEngine;
using FarmingSystem;
using BehaviorTree.PlayerCompanents;

namespace BehaviorTree.Brain {

    [AddComponentMenu("")]
    [MBTNode("Main Companents/" + nameof(FarmingAction))]
    public class FarmingAction : Leaf {
        [SerializeField] private BotBrainDataReference _dataReference;
        [SerializeField] private PlayerReference _playerReference;
        [Space, SerializeField] private BoolReference _showDebugMessage;

        private BotBrainData _botBrainData;
        private FellerCompanent _feller;

        private bool _isEntered = false;

        private bool _fellingStart = false;
        private bool _applyDamage = false;
        private bool _fellingEnd = false;

        private ResourceSpot Source => _botBrainData.NearestResourceSource.CurrentValue;

        public override void OnEnter() {
            base.OnEnter();

            if (_isEntered == true)
                return;

            ResetSequence();

            _botBrainData = _dataReference.Value;
            _feller = _playerReference.Value.Core.Feller;
            _feller.ActionCompleted += OnActionCompleted;
            _feller.Activate(true);

            _isEntered = true;

            if (_showDebugMessage.Value == true)
                Debug.Log($"<color=orange> {title}: enter!</color>");
        }

        public override NodeResult Execute() {

            if (Source == null)
                return NodeResult.failure;

            if (_showDebugMessage.Value == true)
                Debug.Log($"<color=yellow> {title}: execute!</color>");

            return FarmingProcessHandle();
        }

        public override void OnExit() {
            base.OnExit();

            _feller.ActionCompleted -= OnActionCompleted;
            _feller.Activate(false);
            _isEntered = false;

            ResetSequence();

            if (_showDebugMessage.Value == true)
                Debug.Log($"<color=red> {title}: exit!");
        }

        private NodeResult FarmingProcessHandle() {

            if (_botBrainData.BotState.CurrentValue != BotStates.Farming) {
                _botBrainData.SwitchBotState(BotStates.Farming);
                _fellingStart = true;
            }

            FellingExecute();

            if (_fellingStart == false || _applyDamage == false || _fellingEnd == false)
                return NodeResult.running;

            if (_showDebugMessage.Value == true)
                Debug.Log($"<color=green> {title}: succes!</color>");

            return NodeResult.success;
        }

        private void FellingExecute() {

            if (_feller.IsCooldown == true) 
                return;

            _feller.SetTarget(Source);
        }

        private void OnActionCompleted() {
            _applyDamage = true;
            _fellingEnd = true;
        }

        private void ResetSequence() {
            _fellingStart = false;
            _applyDamage = false;
            _fellingEnd = false;
        }
    }
}
