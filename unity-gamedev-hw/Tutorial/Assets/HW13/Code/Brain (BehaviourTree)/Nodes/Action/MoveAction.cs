using MBT;
using System;
using UnityEngine;
using BehaviorTree.PlayerCompanents;

namespace BehaviorTree.Brain {

    [AddComponentMenu("")]
    [MBTNode("Main Companents/" + nameof(MoveAction))]
    public class MoveAction : Leaf {
        [SerializeField] private BotBrainDataReference _dataReference;
        [SerializeField] private PlayerReference _playerReference;
        [SerializeField] private float _stopDistance = 2f;
        [SerializeField] private float _updateInterval = 1f;
        [Space, SerializeField] private BoolReference _showDebugMessage;

        private BotBrainData _botBrainData;
        private MoveCompanent _moveCompanent;
        private float _time = 0;
        private bool _isEntered = false;

        public override void OnEnter() {
            base.OnEnter();

            if (_isEntered == true)
                return;

            _botBrainData = _dataReference.Value;
            _moveCompanent = _playerReference.Value.Core.Mover;

            if (TryGetMoveTarget(out Vector3 targetPosition) == false)
                return;

            _moveCompanent.SetStoppingDistance(_stopDistance);
            _moveCompanent.MoveToTarget(targetPosition);

            _isEntered = true;

            if (_showDebugMessage.Value == true)
                Debug.Log($"<color=orange> {title}: enter!</color>");
        }

        public override void OnExit() {
            base.OnExit();

            _isEntered = false;

            if (_showDebugMessage.Value == true)
                Debug.Log($"<color=red> {title}: exit!</color>");
        }

        public override NodeResult Execute() {

            if (_showDebugMessage.Value == true)
                Debug.Log($"<color=yellow> {title}: execute!</color>");

            _time += Time.deltaTime;

            if (_time > _updateInterval) {
                _time = 0;

                return CheckMoveResult();
            }

            return NodeResult.running;
        }

        private NodeResult CheckMoveResult() {

            var currentMoveStatus = _moveCompanent.GetMoveStatus(out float distanceToTarget);
            _botBrainData.SetDistanceToTarget(distanceToTarget);

            switch (currentMoveStatus) {
                case MoveStatus.PathPending:
                    return NodeResult.running;

                case MoveStatus.HasPath:
                    return NodeResult.running;

                case MoveStatus.PathComplited:
                    _botBrainData.ResetMoveTarget();

                    if (_showDebugMessage.Value == true)
                        Debug.Log($"<color=green> {title}: succes!</color>");

                    return NodeResult.success;

                case MoveStatus.None:

                    if (_showDebugMessage.Value == true)
                        Debug.Log($"<color=green> {title}: MoveStatus.None, DistanceToTarget {distanceToTarget} </color>");

                    return NodeResult.success;

                default:
                    throw new ArgumentException($"Invalid MoveStatus: {status}!");
            }
        }

        private bool TryGetMoveTarget(out Vector3 positon) {

            if (_botBrainData.MoveTarget.CurrentValue == null) {
                positon = Vector3.zero;
                return false;
            }

            positon = _botBrainData.MoveTarget.CurrentValue.position;
            return true;
        }
    }
}
