using System;
using UnityEngine;

namespace ShootEmUp {
    public sealed class EnemyAttackAgent {
        public event Action<BulletSystem.Args> OnFire;

        private Enemy _enemy;
        private BulletSystem.Args _enemyBulletArgs;

        private EnemyMoveAgent _moveAgent;
        private Weapon _weapon;
        private Character _target;
        private float _countdown;
        private float _currentTime;

        public EnemyAttackAgent(Enemy enemy, float countdown) {
            _enemy = enemy;
            _moveAgent = enemy.EnemyMoveAgent;
            _weapon = enemy.Weapon;
            _countdown = countdown;

            CreateDefaultBulletSystemArgs();
        }

        public void SetTarget(Character target) {
            _target = target;
        }

        public void Reset() {
            _currentTime = _countdown;
        }

        public void TryFire() {
            if (_moveAgent.IsReached == false)
                return;

            if (_target.HitPointCounter.IsHitPointsExists() == false)
                return;

            _currentTime -= Time.fixedDeltaTime;

            if (_currentTime <= 0) {
                Fire();
                _currentTime += _countdown;
            }
        }

        private void CreateDefaultBulletSystemArgs() {
            BulletConfig config = _enemy.BulletConfig;

            _enemyBulletArgs = new BulletSystem.Args {
                IsPlayer = false,
                PhysicsLayer = (int)config.PhysicsLayer,
                Color = config.Color,
                Damage = config.Damage,
                Position = Vector2.zero,
                Velocity = Vector2.zero
            };
        }

        private void Fire() {
            var startPosition = _weapon.Position;
            var vector = (Vector2)_target.transform.position - startPosition;
            var direction = vector.normalized;

            _enemyBulletArgs.Position = startPosition;
            _enemyBulletArgs.Velocity = direction;

            OnFire?.Invoke(_enemyBulletArgs);
        }
    }
}