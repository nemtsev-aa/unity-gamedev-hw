using System.Collections.Generic;
using UnityEngine;
using System;

namespace ShootEmUp {
    public sealed class BulletSystem {
        private BulletSpawner _bulletSpawner;
        private LevelBounds _levelBounds;

        private BulletUtils _bulletUtils;
        private readonly Queue<Bullet> _bulletPool = new();
        private readonly HashSet<Bullet> _activeBullets = new();
        private readonly List<Bullet> _cache = new();

        public BulletSystem(BulletSpawner bulletSpawner, LevelBounds levelBounds) {
            _bulletSpawner = bulletSpawner;
            _levelBounds = levelBounds;

            _bulletUtils = new BulletUtils();
        }

        private Transform Container => _bulletSpawner.Container;
        private Transform WorldTransform => _bulletSpawner.WorldTransform;

        public void RemoveBulletsLocatedAbroad() {
            _cache.Clear();
            _cache.AddRange(_activeBullets);

            for (int i = 0, count = _cache.Count; i < count; i++) {
                var bullet = _cache[i];

                if (_levelBounds.InBounds(bullet.transform.position) == false)
                    RemoveBullet(bullet);

            }
        }

        public void FlyBulletByArgs(Args args) {
            
            if (_bulletPool.TryDequeue(out var bullet) == true) 
                bullet.transform.SetParent(WorldTransform);
            else 
                bullet = _bulletSpawner.SpawnBullet(WorldTransform);

            bullet.Init(args);

            if (_activeBullets.Add(bullet) == true) 
                bullet.OnCollisionEntered += OnBulletCollision;
            
        }

        private void OnBulletCollision(Bullet bullet, Unit unit) {
            _bulletUtils.DealDamage(bullet, unit);
            RemoveBullet(bullet);
        }

        private void RemoveBullet(Bullet bullet) {
            
            if (_activeBullets.Remove(bullet)) {
                bullet.OnCollisionEntered -= OnBulletCollision;
                bullet.transform.SetParent(Container);
                
                _bulletPool.Enqueue(bullet);
            }
        }

        public struct Args {
            public Vector2 Position;
            public Vector2 Velocity;
            public Color Color;
            public int PhysicsLayer;
            public int Damage;
            public bool IsPlayer;
        }
    }
}
