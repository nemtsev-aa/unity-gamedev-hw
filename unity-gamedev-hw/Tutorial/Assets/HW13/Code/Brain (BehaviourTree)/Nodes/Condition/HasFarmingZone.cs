using MBT;
using UnityEngine;

namespace BehaviorTree.Brain {

    [AddComponentMenu("")]
    [MBTNode("Main Companents/" + nameof(HasFarmingZone))]
    public partial class HasFarmingZone : Condition {
        [SerializeField] private BotBrainDataReference _dataReference;
        [Space, SerializeField] private BoolReference _showDebugMessage;

        private BotBrainData _brainData;
        private Node _child;

        public override void OnEnter() {
            base.OnEnter();

            _brainData = _dataReference.Value;

            if (children[0] != null)
                _child = children[0];
        }

        public override bool Check() {

            if (_showDebugMessage.Value == true)
                Debug.Log($"<color=yellow> {title}: check </color>");

            return _brainData.NearestFarmingZone.CurrentValue != null;
        }
    }
}



