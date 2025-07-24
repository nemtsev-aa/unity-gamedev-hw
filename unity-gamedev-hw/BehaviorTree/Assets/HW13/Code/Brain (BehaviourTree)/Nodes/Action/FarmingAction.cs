using MBT;
using UnityEngine;
using FarmingSystem;
using BehaviorTree.PlayerVisualSubSystem;

namespace BehaviorTree.Brain {

    [AddComponentMenu("")]
    [MBTNode("Main Companents/" + nameof(FarmingAction))]
    public class FarmingAction : Leaf {
        [SerializeField] private BotBrainDataReference _dataReference;
        [SerializeField] private PlayerReference _playerReference;
        [Space, SerializeField] private BoolReference _showDebugMessage;

        private BotBrainData _botBrainData;
        private PlayerVisual _playerVisual;

        private bool _isEntered = false;

        private bool _farmingStart = false;
        private bool _applyDamage = false;
        private bool _farmingEnd = false;

        private ResourceSpot Source => _botBrainData.NearestResourceSource.CurrentValue;

        public override void OnEnter() {
            base.OnEnter();

            if (_isEntered == true)
                return;

            ResetSequence();

            _botBrainData = _dataReference.Value;
            _playerVisual = _playerReference.Value.Visual;
            _playerVisual.Dispatcher.EventReceived += OnEventReceived;

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

            _playerVisual.Dispatcher.EventReceived -= OnEventReceived;
            _isEntered = false;
            ResetSequence();

            if (_showDebugMessage.Value == true)
                Debug.Log($"<color=red> {title}: exit!");
        }

        private NodeResult FarmingProcessHandle() {
            _botBrainData.SwitchBotState(BotStates.Farming);
            _playerVisual.SetPlayerAnimatorStates(PlayerAnimatorStates.Felling);

            if (_farmingStart == false || _applyDamage == false || _farmingEnd == false)
                return NodeResult.running;

            if (_showDebugMessage.Value == true)
                Debug.Log($"<color=green> {title}: succes!</color>");

            return NodeResult.success;
        }

        private void OnEventReceived(string eventName) {

            switch (eventName) {
                case "FarmingStart":
                    _farmingStart = true;
                    break;

                case "ApplyDamage":
                    Source.TakeDamage();
                    _applyDamage = true;
                    break;

                case "FarmingEnd":
                    _farmingEnd = true;
                    break;

                default:

                    if (_showDebugMessage.Value == true)
                        Debug.Log($"<color=red> Invalid EventName: [{eventName}]");

                    break;
            }
        }

        private void ResetSequence() {
            _farmingStart = false;
            _applyDamage = false;
            _farmingEnd = false;
        }
    }
}
