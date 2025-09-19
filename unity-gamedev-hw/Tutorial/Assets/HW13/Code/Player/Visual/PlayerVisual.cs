using BehaviorTree.PlayerCompanents;
using UnityEngine;

namespace BehaviorTree.PlayerVisualSubSystem {

    public sealed class PlayerVisual : MonoBehaviour {
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public AnimationDispatcher Dispatcher { get; private set; }
        [field: SerializeField] public GameObject VillagerAxe { get; private set; }
        [field: SerializeField] public InventoryCompanentVisual InventoryVisual { get; private set; }

        public bool AttackTimeOut { get; private set; } = false;

        private VisualBehaviour _visualBehaviour;
        private PlayerAnimatorStates _currentState;

        public void Init() {
            _visualBehaviour = new VisualBehaviour(Animator);
        }

        public void SetPlayerAnimatorStates(PlayerAnimatorStates state) {

            if (_currentState == state)
                return;

            _currentState = state;
            //Debug.Log($"PlayerVisual: CurrentState {_currentState}");
            _visualBehaviour.ShowAnimation(_currentState);
        }
    }
}



