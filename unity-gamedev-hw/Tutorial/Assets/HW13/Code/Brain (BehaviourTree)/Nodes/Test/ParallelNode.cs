using MBT;
using UnityEngine;

namespace BehaviorTree.Brain {

    [AddComponentMenu("")]
    [MBTNode("Main Companents/" + nameof(ParallelNode))]
    public class ParallelNode : Composite {

        public override NodeResult Execute() {

            foreach (var iChild in children) {
                iChild?.Execute();
            }

            return NodeResult.success;
        }

        public override void AddChild(Node node) {
            base.AddChild(node);
        }

        public override void RemoveChild(Node node) {
            base.RemoveChild(node);
        }
    }
}
