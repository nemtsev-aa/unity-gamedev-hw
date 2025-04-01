using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp {
    public sealed class BulletSystem : IGameStartListener,
                                       IGameFinishListener,
                                       IGameUpdateListener,
                                       IGamePauseListener {

        private BulletSpawner _bulletSpawner;
        private LevelBounds _levelBounds;
        private float _checkingInterval;
        private float _timer = 0f;

        private BulletUtils _bulletUtils;
        private readonly Queue<Bullet> _bulletPool = new();
        private readonly HashSet<Bullet> _activeBullets = new();
        private readonly List<Bullet> _cache = new();

        public BulletSystem(BulletSpawner bulletSpawner, LevelBounds levelBounds, float positionCheckingInterval) {
            _bulletSpawner = bulletSpawner;
            _levelBounds = levelBounds;
            _checkingInterval = positionCheckingInterval;

            _bulletUtils = new BulletUtils();
        }

        private Transform Container => _bulletSpawner.Container;
        private Transform WorldTransform => _bulletSpawner.WorldTransform;

        public bool IsActive { get; private set; }
        public bool IsPause { get; private set; }

        public void OnStartGame() {
            IsActive = true;
            _timer = _checkingInterval;
        }

        public void OnPauseGame() {
            IsPause = !IsPause;
            StopAllBullets(IsPause);
        }

        public void OnUpdateGame() {
            if (IsActive == false || IsPause == true)
                return;

            _timer -= Time.deltaTime;

            if (_timer <= 0) {
                RemoveBulletsLocatedAbroad();
                _timer = _checkingInterval;
            }
        }

        public void OnFinishGame() {
            IsActive = false;

            _cache.Clear();
            _cache.AddRange(_activeBullets);

            for (int i = 0, count = _cache.Count; i < count; i++) {
                RemoveBullet(_cache[i]);
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

        private void RemoveBulletsLocatedAbroad() {
            _cache.Clear();
            _cache.AddRange(_activeBullets);

            for (int i = 0, count = _cache.Count; i < count; i++) {
                var bullet = _cache[i];

                if (_levelBounds.InBounds(bullet.transform.position) == false)
                    RemoveBullet(bullet);

            }
        }

        private void StopAllBullets(bool status) {
            foreach (Bullet iBullet in _activeBullets) {
                iBullet.SetSleepState(status);
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
