using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp {
    public sealed class EnemyPool : MonoBehaviour {
        [Header("Spawn")]
        [SerializeField] private EnemyPositions _enemyPositions;
        [SerializeField] private Transform _worldTransform;

        [Header("Pool")]
        [SerializeField] private Transform _container;
        [SerializeField] private Enemy _enemyPrefab;

        private int _size;
        private Character _character;
        private EnemyConfig _enemyConfig;
        private readonly Queue<Enemy> _enemyPool = new();

        public void Init(int size, EnemyConfig config,Character character) {
            _size = size;
            _enemyConfig = config;
            _character = character;
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

            var spawnPosition = _enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;

            var attackPosition = _enemyPositions.RandomAttackPosition();
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

            for (var i = 0; i < _size; i++) {
                var enemy = Instantiate(_enemyPrefab, _container);
                enemy.Init(_enemyConfig);

                _enemyPool.Enqueue(enemy);
            }
        }

        private void ClearQueue() {
            foreach (Enemy iEnemy in _enemyPool) {
                Destroy(iEnemy.gameObject);
            }

            _enemyPool.Clear();
        }
    }
}