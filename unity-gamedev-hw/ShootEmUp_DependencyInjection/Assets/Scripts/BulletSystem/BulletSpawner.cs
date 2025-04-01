using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp {

    public sealed class BulletSpawner : IGameStartListener, IGameFinishListener {
        private int _initialCount;
        private BulletFactory _factory;

        private List<Bullet> _bulletPool = new List<Bullet>();

        public Transform Container { get; private set; }
        public Transform WorldTransform { get; private set; }

        public BulletSpawner(BulletFactory bulletFactory, ContainersPresenter presenter, int initialCount) {
            _factory = bulletFactory;
            _initialCount = initialCount;

            Container = presenter.BulletContainer;
            WorldTransform = presenter.WorldContainer;
        }

        public void OnStartGame() {
            CreateBulletPool();
        }

        public void OnFinishGame() {
            ClearBulletPool();
        }

        public Bullet SpawnBullet(Transform parent) {
            return _factory.Get(parent);
        }

        private void CreateBulletPool() {
            if (_initialCount <= 0)
                throw new ArgumentNullException($"Initial Bullet Count is null");

            Container.gameObject.SetActive(false);

            for (var i = 0; i < _initialCount; i++) {
                var bullet = _factory.Get(Container);
                _bulletPool.Add(bullet);
            }
        }

        private void ClearBulletPool() {
            if (_bulletPool != null && _bulletPool.Count > 0) {

                foreach (Bullet iBullet in _bulletPool) {
                    UnityEngine.Object.Destroy(iBullet.gameObject);
                }

                _bulletPool.Clear();
            }
        }
    }
}