using UnityEngine;
using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;
using AtomicFramework.EnemyPointerSystem;
using System.Collections;
using System.Collections.Generic;
using ZombieShooter.SceneObjects;
using ZombieShooter.GameCycleSystem;

namespace AtomicFramework.EnemySystem {

    public sealed class EnemyManager : IContextInit,
                                       IGameStartListener,
                                       IGamePauseListener,
                                       IGameFinishListener {

        private EnemyPool _enemyPool;
        private PointerSystem _pointerSystem;
        private float _spawnDelay;

        private readonly HashSet<Enemy> _activeEnemies = new();

        public bool IsActive { get; private set; }
        public bool IsPause { get; private set; }

        public ReactiveVariable<int> KillEnemyCount { get; private set; } = new ReactiveVariable<int>();

        public void Init(IContext context) {
            _enemyPool = context.GetSystem<EnemyPool>();
            _pointerSystem = context.GetSystem<PointerSystem>();

            _spawnDelay = context.GetEnemySystemConfig().SpawnDelay;
        }

        public void OnStartGame() {
            IsActive = true;

            _enemyPool.CreateEnemyQueue();
            _enemyPool.EnemyPositions.StartCoroutine(StartSpawn());
        }

        public void OnPauseGame() {
            IsPause = !IsPause;
        }

        public void OnFinishGame() {
            IsActive = false;
            KillEnemyCount.Value = 0;

            foreach (Enemy iEnemy in _activeEnemies) {
                iEnemy.Entity.GetIsDestroy().Unsubscribe(OnDestroyed);
                _enemyPool.UnspawnEnemy(iEnemy);
            }

            _activeEnemies.Clear();
            _enemyPool.ClearEnemyQueue();
        }

        private IEnumerator StartSpawn() {

            while (IsActive == true && IsPause == false) {
                yield return new WaitForSeconds(_spawnDelay);

                var IsEnemy = _enemyPool.TrySpawnEnemy(out Enemy enemy);

                if (IsEnemy == true) {

                    if (_activeEnemies.Add(enemy) == true) {
                        enemy.Entity.GetIsDestroy().Subscribe(OnDestroyed);
                        _pointerSystem.AddToList(enemy.EnemyPointer);
                    }
                }
            }
        }

        private void OnDestroyed(IEntity actor) {
            if (actor == null)
                return;

            var enemy = SceneEntity.Cast(actor)?.GetComponent<Enemy>();

            if (enemy == null || _activeEnemies.Remove(enemy) == false)
                return;

            enemy.Entity.GetIsDestroy().Unsubscribe(OnDestroyed);
            _enemyPool.UnspawnEnemy(enemy);
            KillEnemyCount.Value++;
        }
    }
}
