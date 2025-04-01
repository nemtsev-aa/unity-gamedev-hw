using UnityEngine;
using Zenject;

namespace ShootEmUp {
    public sealed class EnemyFactory {
        private DiContainer _container;
        private Enemy _prefab;

        public EnemyFactory(DiContainer container, Enemy prefab) {
            _container = container;
            _prefab = prefab;
        }

        public Enemy Get(Transform parent) {
            Enemy newEnemy = _container.InstantiatePrefabForComponent<Enemy>(_prefab);
            newEnemy.transform.SetParent(parent);

            return newEnemy;
        }
    }
}