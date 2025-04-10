using System;
using UnityEngine;
using System.Collections.Generic;

namespace ShootEmUp {
    public class BulletSpawner : MonoBehaviour {
        private readonly List<Bullet> _bulletPool = new List<Bullet>();
        
        private BulletFactory _factory;
        private int _initialCount;

        [field: SerializeField] public Transform Container { get; private set; }
        [field: SerializeField] public Transform WorldTransform { get; private set; }

        public void Init(BulletFactory bulletFactory, int initialCount) {
            _factory = bulletFactory;
            _initialCount = initialCount;
        }

        public void CreateBulletPool() {
            if (_initialCount <= 0)
                throw new ArgumentNullException($"Initial Bullet Count is null");

            for (var i = 0; i < _initialCount; i++) {
                var bullet = _factory.Get(Container);
                _bulletPool.Add(bullet);
            }
        }

        public Bullet SpawnBullet(Transform parent) {
            return _factory.Get(parent);
        }
    }
}