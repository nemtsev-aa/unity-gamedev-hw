using UnityEngine;
using Zenject;

namespace ShootEmUp {
    public sealed class BulletFactory {
        private DiContainer _container;
        private Bullet _prefab;

        public BulletFactory(DiContainer container, Bullet prefab) {
            _container = container;
            _prefab = prefab;
        }

        public Bullet Get(Transform parent) {
            Bullet newBullet = _container.InstantiatePrefabForComponent<Bullet>(_prefab);
            newBullet.transform.SetParent(parent);

            return newBullet;
        }
    }
}
