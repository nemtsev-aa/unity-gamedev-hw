using System;
using ZombieShooter.SceneObjects;

namespace AtomicFramework.EnemyPointerSystem {

    [Serializable]
    public class EnemyPointer {
        public Enemy Enemy { get; private set; }

        public void Init(Enemy enemy) {
            Enemy = enemy;
        }
    }
}