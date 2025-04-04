using System.Collections.Generic;
using Zenject;

namespace ShootEmUp {

    public class GameCycle : ILateTickable, ITickable, IFixedTickable {
        private List<IGameListener> _gameListeners = new();

        private List<IGameLateUpdateListener> _gameLateUpdateListeners = new();
        private List<IGameUpdateListener> _gameUpdateListeners = new();
        private List<IGameFixedUpdateListener> _gameFixedUpdateListeners = new();

        public GameCycle() {
            CurrentState = GameStates.InitializingComponents;
        }

        public GameStates CurrentState { get; private set; } 

        public void SetCurrentState(GameStates state) {
            CurrentState = state;
        }

        public void Add(IGameListener listener) {
            if (_gameListeners.Contains(listener) == false)
                _gameListeners.Add(listener);

            if (listener is IGameLateUpdateListener gameLateUpdateListener) {
                _gameLateUpdateListeners.Add(gameLateUpdateListener);
                return;
            }

            if (listener is IGameUpdateListener gameUpdateListener) {
                _gameUpdateListeners.Add(gameUpdateListener);
                return;
            }

            if (listener is IGameFixedUpdateListener gameFixedUpdateListener) {
                _gameFixedUpdateListeners.Add(gameFixedUpdateListener);
                return;
            }
        }

        public void StartGame() {
            if (_gameListeners.Count == 0)
                return;

            if (CurrentState != GameStates.WaitingToStart)
                return;

            foreach (var iListener in _gameListeners) {

                if (iListener is IGameStartListener gameStartListener)
                    gameStartListener.OnStartGame();
            }

            SetCurrentState(GameStates.Playing);
        }

        public void PauseGame() {
            if (_gameListeners.Count == 0)
                return;

            foreach (var iListener in _gameListeners) {

                if (iListener is IGamePauseListener gamePauseListener)
                    gamePauseListener.OnPauseGame();
            }

            if (CurrentState == GameStates.Playing) {
                SetCurrentState(GameStates.Pause); 
                return;
            }

            if (CurrentState == GameStates.Pause)
                SetCurrentState(GameStates.Playing);
        }

        public void FinishGame() {

            if (_gameListeners.Count == 0)
                return;

            foreach (var iListener in _gameListeners) {

                if (iListener is IGameFinishListener gameFinishListener)
                    gameFinishListener.OnFinishGame();
            }

            SetCurrentState(GameStates.WaitingToStart);
        }

        public void LateTick() {
            if (CurrentState == GameStates.InitializingComponents)
                return;

            if (_gameLateUpdateListeners.Count == 0)
                return;

            foreach (var iListener in _gameLateUpdateListeners) {
                iListener.OnLateUpdateGame();
            }
        }

        public void Tick() {
            if (CurrentState == GameStates.InitializingComponents)
                return;

            if (_gameUpdateListeners.Count == 0)
                return;

            foreach (var iListener in _gameUpdateListeners) {
                iListener.OnUpdateGame();
            }
        }

        public void FixedTick() {
            if (CurrentState == GameStates.InitializingComponents)
                return;

            if (_gameFixedUpdateListeners.Count == 0)
                return;

            foreach (var iListener in _gameFixedUpdateListeners) {
                iListener.OnFixedUpdateGame();
            }
        }

    }
}



