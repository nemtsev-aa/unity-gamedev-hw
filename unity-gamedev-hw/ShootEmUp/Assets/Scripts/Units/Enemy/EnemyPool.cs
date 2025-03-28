using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp {
    public sealed class EnemyPool : MonoBehaviour {
        [Header("Spawn")]
        [SerializeField] private int _size = 7;
        [SerializeField] private EnemyPositions _enemyPositions;
        [SerializeField] private Transform _worldTransform;

        [Header("Pool")]
        [SerializeField] private Transform _container;
        [SerializeField] private Enemy _enemyPrefab;

        private Character _character;
        private EnemyConfig _enemyConfig;
        private readonly Queue<Enemy> _enemyPool = new();

        public void Init(Character character, EnemyConfig config) {
            _character = character;
            _enemyConfig = config;
        }

        public void CreateEnemyQueue() {
            CreateQueue();
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
    }
}