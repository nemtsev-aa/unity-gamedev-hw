using MBT;
using UnityEngine;

namespace BehaviorTree.Brain {

    [AddComponentMenu("")]
    [MBTNode("Main Companents/" + nameof(TestNode))]
    public class TestNode : Leaf {
        public bool _result;
        public BotBrainDataReference _data;
        
        public override NodeResult Execute() {

            Debug.Log($"Execute {title}");

            var botState = _data.Value.BotState;
            _result = botState != null;

            Debug.Log($"Execute {title}. Result [{botState}]");

            if (_result == true)
                return NodeResult.success;
            else
                return NodeResult.failure;
        }
    }
}
