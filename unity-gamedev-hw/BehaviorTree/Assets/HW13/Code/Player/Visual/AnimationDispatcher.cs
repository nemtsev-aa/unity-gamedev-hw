using System;
using UnityEngine;

namespace BehaviorTree.PlayerVisualSubSystem {

    public sealed class AnimationDispatcher : MonoBehaviour {
        
        public event Action<string> EventReceived;

        public void ReceiveEvent(string key) {
            EventReceived?.Invoke(key);
        }
    }
}