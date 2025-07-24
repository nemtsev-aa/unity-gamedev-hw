using UnityEngine;

namespace Conveyors.Entity.Visual {

    [AddComponentMenu("Gameplay/Conveyors/Conveyor Visual")]
    public sealed class ConveyorAnimator : MonoBehaviour {
        private static readonly int STATE = Animator.StringToHash("State");

        private const int IDLE_ANIMATION = 0;
        private const int SAW_ANIMATION = 1;

        [Space]
        [SerializeField] private Animator _workerAnimator;
        [SerializeField] private GameObject _sawObject;
        [SerializeField] private GameObject _woodObject;

        private void Awake() {
            _sawObject.SetActive(false);
            _woodObject.SetActive(false);
        }

        public void Play() {
            _workerAnimator.SetInteger(STATE, SAW_ANIMATION);
            _sawObject.SetActive(true);
            _woodObject.SetActive(true);
        }

        public void Stop() {
            _workerAnimator.SetInteger(STATE, IDLE_ANIMATION);
            _sawObject.SetActive(false);
            _woodObject.SetActive(false);
        }
    }
}