using Atomic.Elements;

namespace ZombieShooter.UI {

    public sealed class PlayerInfoViewModel {

        public PlayerInfoViewModel(ReactiveVariable<float> hitPoints,
                                   ReactiveVariable<int> bulletAmount,
                                   ReactiveVariable<int> maxBulletAmount,
                                   ReactiveVariable<int> killEnemyCount) {

            HitPoints = hitPoints;
            BulletAmount = bulletAmount;
            MaxBulletAmount = maxBulletAmount;
            KillEnemyCount = killEnemyCount;
        }

        public ReactiveVariable<float> HitPoints { get; private set; }
        public ReactiveVariable<int> BulletAmount { get; private set; }
        public ReactiveVariable<int> MaxBulletAmount { get; private set; }
        public ReactiveVariable<int> KillEnemyCount { get; private set; }
    }
}
