using UnityEngine;

namespace CharactersSystem.Player.Components {
    
    [RequireComponent(typeof(Animator))]
    public class AnimationHandler : MonoBehaviour {
        public const string STATE_INDEX = "StateIndex";

        private Animator _animator;

        public void Init() {
            _animator = GetComponent<Animator>();
        }

        public void SetStateIndex(float speed) {

            if (speed == 0f) {
                _animator.SetFloat(STATE_INDEX, 0f);
                return;
            }

            _animator.SetFloat(STATE_INDEX, 0.5f);
        }
    }
}
