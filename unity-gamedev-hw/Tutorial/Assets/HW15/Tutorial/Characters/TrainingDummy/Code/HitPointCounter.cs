using System;

namespace Characters {

    [Serializable]
    public sealed class HitPointCounter {
        public event Action HitPointsEmpty;
        public event Action<int> HitPointsChanged;

        private int _defaultHitPoints;
        private int _hitPoints;

        public void Init(int maxValue) {
            _defaultHitPoints = _hitPoints = maxValue;
        }

        public bool IsHitPointsExists() {
            return _hitPoints > 0;
        }

        public void TakeDamage(int damage) {
            _hitPoints -= damage;

            HitPointsChanged?.Invoke(_hitPoints);

            if (_hitPoints <= 0) {
                _hitPoints = 0;
                HitPointsEmpty?.Invoke();
            }
        }

        public void Reset() {
            _hitPoints = _defaultHitPoints;
            HitPointsChanged?.Invoke(_hitPoints);
        }
    }
}