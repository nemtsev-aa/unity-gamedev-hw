using MBT;
using UnityEngine;

namespace BehaviorTree.Brain {

    [AddComponentMenu("")]
    [MBTNode("Main Companents/" + nameof(IsPatrol))]
    public partial class IsPatrol : Condition {
        [SerializeField] private BotBrainDataReference _dataReference;
        [Space, SerializeField] private BoolReference _showDebugMessage;

        private Node _child;
        private BotBrainData _brainData => _dataReference.Value;

        public override void OnEnter() {
            base.OnEnter();

            if (children[0] != null)
                _child = children[0];
        }

        public override bool Check() {

            if (_showDebugMessage.Value == true)
                Debug.Log($"<color=yellow> {title}: check </color>");

            bool hasZone = _brainData.NearestFarmingZone.CurrentValue != null;
            bool loot = _brainData.CurrentInventoryAmount.CurrentValue > 0;

            if (hasZone == false && loot == false) {
                return true;
            }

            return false;
        }
    }
}



