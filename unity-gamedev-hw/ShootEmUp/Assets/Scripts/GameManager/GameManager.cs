using UnityEngine;
using System;

namespace ShootEmUp {
    public sealed class GameManager : MonoBehaviour, IDisposable {
        private Character _character;
        private EnemyManager _enemyManager;
        private BulletSystem _bulletSystem;
        private bool _isInit;
        
        public void Init(Character character, EnemyManager enemyManager, BulletSystem bulletSystem) {
            _character = character;
            _enemyManager = enemyManager;
            _bulletSystem = bulletSystem;
            
            AddListeners();
            _isInit = true;
        }

        public void StartGame() {
            _enemyManager.SpawnEnemy();
        }

        public void FinishGame(Unit actor) {
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }

        private void FixedUpdate() {
            if (_isInit == true)
                _bulletSystem.RemoveBulletsLocatedAbroad();
        }

        private void AddListeners() {
            _character.Death += FinishGame;
        }

        private void RemoveListeners() {
            _character.Death -= FinishGame;
        }

        public void Dispose() {
            RemoveListeners();
        }
    }
}