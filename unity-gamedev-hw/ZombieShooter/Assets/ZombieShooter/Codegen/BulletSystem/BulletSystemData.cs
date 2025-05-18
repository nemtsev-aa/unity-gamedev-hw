using System;
using Atomic.Elements;
using AtomicFramework.EntityPool;

namespace AtomicFramework.BulletSystem {

    [Serializable]
    public sealed class BulletSystemData {

        public BulletSystemData(IEntityPool pool, BulletConfig config, FirePoint firePoint, Cycle spawnCycle) {
            Pool = pool;
            Config = config;
            FirePoint = firePoint;
            SpawnCycle = spawnCycle;
        }

        public IEntityPool Pool { get; private set; }
        public BulletConfig Config { get; private set; }
        public FirePoint FirePoint { get; private set; }
        public Cycle SpawnCycle { get; private set; }
    }
}
