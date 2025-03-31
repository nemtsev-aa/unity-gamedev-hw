using System;

namespace ShootEmUp {
    public sealed class HitPointCounter {
        public event Action HitPointsEmpty;

        private int _hitPoints;

        public HitPointCounter(int hitPoints) {
            _hitPoints = hitPoints;
        }

        public bool IsHitPointsExists() {
            return _hitPoints > 0;
        }

        public void TakeDamage(int damage) {
            _hitPoints -= damage;

            if (_hitPoints <= 0) {
                _hitPoints = 0;
                HitPointsEmpty?.Invoke();
            }
        }
    }
}