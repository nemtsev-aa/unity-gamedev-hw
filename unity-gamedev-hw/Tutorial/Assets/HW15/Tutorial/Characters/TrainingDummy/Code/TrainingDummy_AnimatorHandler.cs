using System;
using UnityEngine;

namespace Characters {
    [Serializable]
    public sealed class TrainingDummy_AnimatorHandler {
        private const int STATE = 0;
        private const int IDLE_INDEX = 0;
        private const int DAMAGE_INDEX = 1;
        private const int DESTROY_INDEX = 2;

        private const string DAMAGE_ID = "Damage";
        private const string DESTROY_ID = "Destroy";

        [SerializeField] private Animator _animator;

        //public void ShowIdleAnimation() =>
        //    _animator.SetFloat(STATE, IDLE_INDEX);

        //public void ShowDamageAnimation() =>
        //    _animator.SetFloat(STATE, DAMAGE_INDEX);

        //public void ShowDestroyAnimation() =>
        //    _animator.SetFloat(STATE, DESTROY_INDEX); 

        public void ShowIdleAnimation() {
            _animator.ResetTrigger(DAMAGE_ID);
            _animator.ResetTrigger(DESTROY_ID);
        }

        public void ShowDamageAnimation() =>
            _animator.SetTrigger(DAMAGE_ID);

        public void ShowDestroyAnimation() =>
            _animator.SetTrigger(DESTROY_ID);
    }
}