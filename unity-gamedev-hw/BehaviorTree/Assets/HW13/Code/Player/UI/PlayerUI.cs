using System;
using UnityEngine;

namespace BehaviorTree.PlayerCoreSubsystem {

    [Serializable]
    public sealed class PlayerUI : MonoBehaviour {
        [field: SerializeField] public RectTransform UIRoot { get; private set; }

        public void Init() {
            
        }
    }
}