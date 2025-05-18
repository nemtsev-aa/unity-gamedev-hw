using System;
using UnityEngine;
using Atomic.Contexts;
using System.Collections.Generic;
using ZombieShooter.SceneObjects;

namespace AtomicFramework.EnemySystem {

    public sealed class EnemyPool : IContextInit {
        private IContext _context;
        private EnemyConfig _enemyConfig;
        private EnemyFactory _factory;
        private EnemyPositions _enemyPositions;
        private Transform _worldTransform;
        private Transform _container;
        private int _size;
        private Transform _target;

        private readonly Queue<Enemy> _enemyPool = new();

        public EnemyPositions EnemyPositions => _enemyPositions;

        public void Init(IContext context) {
            _context = context;

            _enemyConfig = context.GetEnemyConfig();
            _factory = context.GetSystem<EnemyFactory>();
            _enemyPositions = context.GetEnemyPositions();

            _worldTransform = context.GetContainersPresenter().WorldContainer;
            _container = context.GetContainersPresenter().EnemyContainer;
            _size = context.GetEnemySystemConfig().PoolSize;

            
        }

        public void CreateEnemyQueue() {
            CreateQueue();
        }

        public void ClearEnemyQueue() {

            if (_enemyPool.Count <= 0)
                return;

            ClearQueue();
        }

        public bool TrySpawnEnemy(out Enemy enemy) {

            if (_enemyPool.TryDequeue(out enemy) == false)
                return false;

            
            enemy.transform.SetParent(_worldTransform);
            var spawnPosition = EnemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;

            return true;
        }

        public void UnspawnEnemy(Enemy enemy) {
            enemy.Reset();
            enemy.transform.SetParent(_container);
            _enemyPool.Enqueue(enemy);
        }

        private void CreateQueue() {

            if (_size <= 0)
                throw new ArgumentNullException($"EnemyPool size less than zero!");

            _container.gameObject.SetActive(false);
            _target = _context.GetCharacter().transform;

            for (var i = 0; i < _size; i++) {
                var enemy = _factory.Get(_container);
                enemy.Init(_enemyConfig, _target);

                _enemyPool.Enqueue(enemy);
            }
        }

        private void ClearQueue() {

            if (_enemyPool == null)
                return;

            foreach (Enemy iEnemy in _enemyPool) {
                UnityEngine.Object.Destroy(iEnemy.gameObject);
            }

            _enemyPool.Clear();
        }
    }
}