using Atomic.Elements;

namespace ZombieShooter.UI {
    public sealed class GameplayPopupViewModel {
        private ReactiveVariable<float> _hitPoints;
        private ReactiveVariable<int> _bulletAmount;
        private ReactiveVariable<int> _maxBulletAmount;
        private ReactiveVariable<int> _killEnemyCount;

        public GameplayPopupViewModel(ReactiveVariable<float> hitPoints, ReactiveVariable<int> bulletAmount, ReactiveVariable<int> maxBulletAmount, ReactiveVariable<int> killEnemyCount) {
            _hitPoints = hitPoints;
            _bulletAmount = bulletAmount;
            _maxBulletAmount = maxBulletAmount;
            _killEnemyCount = killEnemyCount;

            PlayerInfoViewModel = CreatePlayerInfoViewModel();
        }

        public PlayerInfoViewModel PlayerInfoViewModel { get; private set; }

        private PlayerInfoViewModel CreatePlayerInfoViewModel() {
            return new PlayerInfoViewModel(
                    _hitPoints,
                    _bulletAmount,
                    _maxBulletAmount,
                    _killEnemyCount);
        }
    }

}

