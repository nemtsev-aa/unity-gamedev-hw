using BehaviorTree.PlayerCompanents;
using UnityEngine;

namespace BehaviorTree.PlayerVisualSubSystem {

    public sealed class PlayerVisual : MonoBehaviour {
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public AnimationDispatcher Dispatcher { get; private set; }
        [field: SerializeField] public GameObject VillagerAxe { get; private set; }
        [field: SerializeField] public InventoryCompanentVisual InventoryVisual { get; private set; }

        private VisualBehaviour _visualBehaviour;

        public void Init() {
            _visualBehaviour = new VisualBehaviour(Animator);
        }

        public void SetPlayerAnimatorStates(PlayerAnimatorStates state) {
            _visualBehaviour.ShowAnimation(state);
        }
    }
}



