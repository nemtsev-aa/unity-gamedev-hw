using System;
using UnityEngine;

namespace AtomicFramework.View.Visual {

    public class AnimationDispatcher : MonoBehaviour {
        public event Action<string> OnEventReceived;

        public void ReceiveEvent(string key) {
            OnEventReceived?.Invoke(key);
        }
    }
}