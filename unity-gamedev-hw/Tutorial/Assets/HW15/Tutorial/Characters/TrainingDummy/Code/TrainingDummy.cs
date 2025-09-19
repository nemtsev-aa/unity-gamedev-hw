using FarmingSystem;
using System;
using UnityEngine;

namespace Characters {

    public sealed class TrainingDummy : Character, IExtractive {
        public event Action Destroyed;

        [Space, SerializeField] private CharacterConfig _config;
        [SerializeField] private HitPointCounter _hitPointCounter;
        [SerializeField] private TrainingDummy_AnimatorHandler _animatorHandler;
        [SerializeField] private HealthBar _healthBar;

        private void Start() {
            SetState(State);
        }

        public override void SetState(CharacterStates state) {
            base.SetState(state);

            if (state == CharacterStates.Idle) {
                _hitPointCounter.Init(_config.HitPointMaxValue);
                _hitPointCounter.HitPointsChanged += HitPointCounter_HitPointsChanged;

                _hitPointCounter.Reset();
                _animatorHandler.ShowIdleAnimation();

                return;
            }
        }

        public void Extract(int damage = 1) {
            _hitPointCounter.TakeDamage(damage);
        }

        private void HitPointCounter_HitPointsChanged(int value) {

            if (value <= 0) {
                _animatorHandler.ShowDestroyAnimation();
                _healthBar.Show(false);

                Destroyed?.Invoke();
                return;
            }

            var percent = ((float)value / _config.HitPointMaxValue);
            _healthBar.UpdateFiller(percent);

            _animatorHandler.ShowDamageAnimation();
        }
    }
}