using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp {
    public sealed class EnemyManager {

        private BulletSystem _bulletSystem;
        private EnemyPool _enemyPool;
        private float _spawnDelay;

        
        private readonly HashSet<Enemy> _activeEnemies = new();

        public EnemyManager(BulletSystem bulletSystem, EnemyPool enemyPool, float spawnDelay) {
            _bulletSystem = bulletSystem;
            _enemyPool = enemyPool;
            _spawnDelay = spawnDelay;
        }

        public void SpawnEnemy() {
            _enemyPool.CreateEnemyQueue();
            _enemyPool.StartCoroutine(StartSpawn());
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
            _bulletSystem.FlyBulletByArgs(_enemyBulletArgs);
        }
    }
}
