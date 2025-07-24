using MBT;
using UnityEngine;

namespace BehaviorTree.Brain {

    [AddComponentMenu("")]
    [MBTNode("Main Companents/" + nameof(DecisionNode))]
    public class DecisionNode : Composite {
        [SerializeField] private int _operationIndex;

        public override NodeResult Execute() {

            if (children[_operationIndex] != null) {
                var result = children[_operationIndex].Execute();

                Debug.Log($"OperationIndex [{_operationIndex}] {result}");

                return NodeResult.success;
            }

            return NodeResult.failure;
        }

        #region Validation
        public override void AddChild(Node node) {
            base.AddChild(node);

        }

        public override void RemoveChild(Node node) {
            base.RemoveChild(node);

        }
        #endregion
    }
}
