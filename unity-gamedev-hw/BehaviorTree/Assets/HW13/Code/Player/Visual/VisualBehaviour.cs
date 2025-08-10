using System;
using UnityEngine;
using System.Collections.Generic;

namespace BehaviorTree.PlayerVisualSubSystem {

    [Serializable]
    public sealed class VisualBehaviour {
        private const string STATE_INDEX = "StateIndex";

        private readonly Animator _animator;

        private readonly Dictionary<PlayerAnimatorStates, float> _animationDictionary = new Dictionary<PlayerAnimatorStates, float> {
            { PlayerAnimatorStates.Idle, 0f },
            { PlayerAnimatorStates.MoveToForest, 0.5f },
            { PlayerAnimatorStates.Delivery, 0.5f },
            { PlayerAnimatorStates.Felling, 1f }
        };

        public VisualBehaviour(Animator animator) {
            _animator = animator;
        }

        public void ShowAnimation(PlayerAnimatorStates state) {
            float stateValue = _animationDictionary[state];
            _animator.SetFloat(STATE_INDEX, stateValue);
        }
    }
}



