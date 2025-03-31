using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp {

    public class GameCycle : MonoBehaviour {
        private List<IGameListener> _gameListeners = new();
 
        public void Add(IGameListener listener) {
            if (_gameListeners.Contains(listener) == false)
                _gameListeners.Add(listener);
        }

        public void StartGame() {
            if (_gameListeners.Count == 0)
                throw new ArgumentNullException("GameListeners list is empty!");

            foreach (var iListener in _gameListeners) {

                if (iListener is IGameStartListener gameStartListener)
                    gameStartListener.OnStartGame();
            }
        }

        public void PauseGame() {
            if (_gameListeners.Count == 0)
                throw new ArgumentNullException("GameListeners list is empty!");

            foreach (var iListener in _gameListeners) {

                if (iListener is IGamePauseListener gamePauseListener)
                    gamePauseListener.OnPauseGame();
            }
        }

        public void FinishGame() {

            if (_gameListeners.Count == 0)
                throw new ArgumentNullException("GameListeners list is empty!");

            foreach (var iListener in _gameListeners) {

                if (iListener is IGameFinishListener gameFinishListener)
                    gameFinishListener.OnFinishGame();
            }
        }

        private void LateUpdate() {
            if (_gameListeners.Count == 0)
                throw new ArgumentNullException("GameListeners list is empty!");

            foreach (var iListener in _gameListeners) {

                if (iListener is IGameLateUpdateListener gameLateUpdateListener)
                    gameLateUpdateListener.OnLateUpdateGame();
            }
        }

        private void Update() {
            if (_gameListeners.Count == 0)
                throw new ArgumentNullException("GameListeners list is empty!");

            foreach (var iListener in _gameListeners) {

                if (iListener is IGameUpdateListener gameUpdateListener)
                    gameUpdateListener.OnUpdateGame();

            }
        }

        private void FixedUpdate() {
            if (_gameListeners.Count == 0)
                throw new ArgumentNullException("GameListeners list is empty!");

            foreach (var iListener in _gameListeners) {

                if (iListener is IGameFixedUpdateListener gameFixedUpdateListener)
                    gameFixedUpdateListener.OnFixedUpdateGame();

            }
        }
    }
}



