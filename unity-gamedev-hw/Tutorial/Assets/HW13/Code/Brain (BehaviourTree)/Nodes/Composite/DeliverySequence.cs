using MBT;
using System;
using UnityEngine;

namespace BehaviorTree.Brain {

    [AddComponentMenu("")]
    [MBTNode("Main Companents/" + nameof(DeliverySequence))]
    public class DeliverySequence : Composite {
        [SerializeField] private BotBrainDataReference _dataReference;
        [Space, SerializeField] private BoolReference _showDebugMessage;

        private BotBrainData _botBrainData;

        private bool _moveSucces = false;
        private bool _dropSucces = false;
        private bool _waitSucces = false;

        private Node MoveNode => children[0];
        private Node DropNode => children[1];
        private Node WaitNode => children[2];

        public override void OnEnter() {
            base.OnEnter();

            if (MoveNode == null || DropNode == null || WaitNode == null)
                throw new ArgumentException($"{title}: OnValididation failed!");

            _botBrainData = _dataReference.Value;

            if (_showDebugMessage.Value == true)
                Debug.Log($"<color=orange> {title}: enter!</color>");
        }

        public override NodeResult Execute() {

            if (_showDebugMessage.Value == true)
                Debug.Log($"<color=yellow> {title}: execute!</color>");

            if (_moveSucces == false) {

                MoveNode.OnEnter();
                _moveSucces = NodeExecute(MoveNode, out var moveActionResult);

                if (_moveSucces == false)
                    return moveActionResult;
            }

            if (_dropSucces == false) {

                DropNode.OnEnter();
                _dropSucces = NodeExecute(DropNode, out var collectActionResult);

                if (_dropSucces == false)
                    return collectActionResult;
            }

            if (_waitSucces == false) {

                _waitSucces = NodeExecute(WaitNode, out var waitActionResult);

                if (_waitSucces == false)
                    return waitActionResult;
            }

            if (_moveSucces == _dropSucces == _waitSucces == true) {

                if (_showDebugMessage.Value == true)
                    Debug.Log($"<color=green> {title}: Success!");

                return NodeResult.success;
            }

            return NodeResult.running;
        }

        public override void OnExit() {
            base.OnExit();

            ResetSequence();
        }

        private bool NodeExecute(Node node, out NodeResult result) {

            var nodeResult = node.Execute();

            if (nodeResult == NodeResult.success) {

                if (_showDebugMessage.Value == true)
                    Debug.Log($"<color=green> {title}: {node.title} {nodeResult.status}");

                result = nodeResult;
                return true;
            }

            if (_showDebugMessage.Value == true)
                Debug.Log($"<color=yellow> {title}: {node.title} {nodeResult.status}");

            result = nodeResult;
            return false;
        }

        private void ResetSequence() {
            _moveSucces = false;
            _dropSucces = false;
            _waitSucces = false;

            MoveNode.OnExit();
            DropNode.OnExit();

            _botBrainData.UpdateCurrentInventoryAmount(0);
        }
    }
}
