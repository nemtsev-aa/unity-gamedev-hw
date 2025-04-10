using System;

namespace ShootEmUp {
    public sealed class HitPointCounter {
        public event Action HitPointsEmpty;

        private readonly int _defaulthitPoints;
        private int _hitPoints;

        public HitPointCounter(int hitPoints) {
            _defaulthitPoints = _hitPoints = hitPoints;
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

        public void Reset() {
            _hitPoints = _defaulthitPoints;
        }
    }
}