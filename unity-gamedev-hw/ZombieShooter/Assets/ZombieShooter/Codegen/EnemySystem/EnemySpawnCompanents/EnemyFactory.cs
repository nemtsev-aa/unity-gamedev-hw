using Atomic.Contexts;
using UnityEngine;
using ZombieShooter.SceneObjects;

namespace AtomicFramework.EnemySystem {

    public sealed class EnemyFactory : IContextInit {
        private Enemy _prefab;

        public void Init(IContext context) {
            _prefab = context.GetEnemySystemConfig().Prefab;
        }

        public Enemy Get(Transform parent) {
            Enemy newEnemy = Object.Instantiate(_prefab);
            newEnemy.transform.SetParent(parent);

            return newEnemy;
        }
    }
}