using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp {
    public sealed class EnemyPool {
        private int _size;

        private Transform _container;
        private Transform _worldTransform;
        private EnemyConfig _enemyConfig;
        private EnemyFactory _factory;

        private Character _character;

        private readonly Queue<Enemy> _enemyPool = new();

        public EnemyPositions EnemyPositions { get; private set; }

        public EnemyPool(EnemyConfig config, EnemyFactory factory, EnemyPositions enemyPositions,
                              ContainersPresenter presenter, Character character, int size) {

            _enemyConfig = config;
            _factory = factory;
            EnemyPositions = enemyPositions;
            _character = character;
            _container = presenter.EnemyContainer;
            _worldTransform = presenter.WorldContainer;
            _size = size;
        }

        public void CreateEnemyQueue() {
            CreateQueue();
        }

        public void ClearEnemyQueue() {
            if (_enemyPool.Count <= 0)
                return;

            ClearQueue();
        }

        public Enemy SpawnEnemy() {
            if (_enemyPool.TryDequeue(out var enemy) == false)
                return null;

            enemy.transform.SetParent(_worldTransform);

            var spawnPosition = EnemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;

            var attackPosition = EnemyPositions.RandomAttackPosition();
            enemy.EnemyMoveAgent.SetDestination(attackPosition.position);
            enemy.EnemyAttackAgent.SetTarget(_character);

            return enemy;
        }

        public void UnspawnEnemy(Enemy enemy) {
            enemy.transform.SetParent(_container);
            _enemyPool.Enqueue(enemy);
        }

        private void CreateQueue() {
            if (_size <= 0)
                throw new ArgumentNullException($"EnemyPool size less than zero!");

            _container.gameObject.SetActive(false);

            for (var i = 0; i < _size; i++) {
                var enemy = _factory.Get(_container);
                enemy.Init(_enemyConfig);

                _enemyPool.Enqueue(enemy);
            }
        }

        private void ClearQueue() {
            foreach (Enemy iEnemy in _enemyPool) {
                UnityEngine.Object.Destroy(iEnemy.gameObject);
            }

            _enemyPool.Clear();
        }
    }
}