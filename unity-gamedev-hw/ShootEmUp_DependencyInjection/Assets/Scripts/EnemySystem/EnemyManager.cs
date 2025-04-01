using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp {
    public sealed class EnemyManager : IGameStartListener, IGameFinishListener, IGamePauseListener, IGameFixedUpdateListener {
        private BulletSystem _bulletSystem;
        private EnemyPool _enemyPool;
        private float _spawnDelay;

        private readonly HashSet<Enemy> _activeEnemies = new();

        public bool IsActive { get; private set; }
        public bool IsPause { get; private set; }

        public EnemyManager(BulletSystem bulletSystem, EnemyPool enemyPool, float spawnDelay) {
            _bulletSystem = bulletSystem;
            _enemyPool = enemyPool;
            _spawnDelay = spawnDelay;
        }

        public void OnStartGame() {
            IsActive = true;

            _enemyPool.CreateEnemyQueue();
            _enemyPool.EnemyPositions.StartCoroutine(StartSpawn());
        }

        public void OnPauseGame() {
            IsPause = !IsPause;
        }

        public void OnFixedUpdateGame() {
            if (IsPause == true)
                return;

            foreach (Enemy iEnemy in _activeEnemies) {
                iEnemy.UpdateState();
            }
        }

        public void OnFinishGame() {
            IsActive = false;

            foreach (Enemy iEnemy in _activeEnemies) {
                iEnemy.Death -= OnDestroyed;
                iEnemy.EnemyAttackAgent.OnFire -= OnFire;

                _enemyPool.UnspawnEnemy(iEnemy);
            }

            _activeEnemies.Clear();
            _enemyPool.ClearEnemyQueue();
        }

        private IEnumerator StartSpawn() {
            while (true) {
                yield return new WaitForSeconds(_spawnDelay);

                var enemy = _enemyPool.SpawnEnemy();

                if (enemy != null) {

                    if (_activeEnemies.Add(enemy) == true) {
                        enemy.Death += OnDestroyed;
                        enemy.EnemyAttackAgent.OnFire += OnFire;
                    }
                }
            }
        }

        private void OnDestroyed(Unit actor) {
            Enemy enemy = (Enemy)actor;

            if (_activeEnemies.Remove(enemy) == true) {
                enemy.Death -= OnDestroyed;
                enemy.EnemyAttackAgent.OnFire -= OnFire;

                _enemyPool.UnspawnEnemy(enemy);
            }
        }

        private void OnFire(BulletSystem.Args _enemyBulletArgs) {
            if (IsPause == true)
                return;

            _bulletSystem.FlyBulletByArgs(_enemyBulletArgs);
        }
    }
}
