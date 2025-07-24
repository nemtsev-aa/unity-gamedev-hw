using UnityEngine;

namespace BehaviorTree.Brain {

    public sealed class BotBrainDataProvider : MonoBehaviour {
        public BotBrainData Data { get; private set; }
        
        public void Init(BotBrainData data) {
            Data = data;
        }
    }
}



