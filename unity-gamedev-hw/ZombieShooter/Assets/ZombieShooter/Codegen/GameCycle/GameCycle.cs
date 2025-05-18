using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;
using AtomicFramework.CameraFollowSystem;
using AtomicFramework.EnemyPointerSystem;
using AtomicFramework.EnemySystem;
using AtomicFramework.InputSystem;
using System.Collections.Generic;
using UnityEngine;
using ZombieShooter.SceneObjects;
using ZombieShooter.UI;

namespace ZombieShooter.GameCycleSystem {

    public class GameCycle : IContextInit, IContextDispose {
        private IContext _context;

        private IEvent<GameStates> _gameStateChangeAction;
        private BaseEvent<IEntity> _characterDestroyAction;

        private List<IGameStartListener> _startListeners;
        private List<IGamePauseListener> _pauseListeners = new();
        private List<IGameFinishListener> _finishListeners = new();

        private bool _showLogAfterExecution = false;

        public GameStates CurrentState { get; private set; }

        public void Init(IContext context) {
            _context = context;

            InitSystems();
            CreateSubscribes();

            if (_showLogAfterExecution == true)
                Debug.Log($"{nameof(GameCycle)} installed!");
        }

        private void InitSystems() {

            _startListeners = new List<IGameStartListener>() {
                _context.GetSystem<UIController>(),
                _context.GetSystem<CharacterSpawner>(),
                _context.GetSystem<EnemyManager>(),
                _context.GetSystem<CharacterInputHandler>(),
                _context.GetSystem<PointerSystem>(),
                _context.GetSystem<CameraFollow>()
            };

            foreach (var iSystem in _context.Systems) {

                if (iSystem is IGamePauseListener pauseListener)
                    _pauseListeners.Add(pauseListener);


                if (iSystem is IGameFinishListener finishListener)
                    _finishListeners.Add(finishListener);
            }
        }

        private void CreateSubscribes() {
            _gameStateChangeAction = _context.GetGameStateChangeAction();
            _gameStateChangeAction.Subscribe(SetCurrentState);

            _gameStateChangeAction?.Invoke(GameStates.WaitingToStart);
        }

        private void SetCurrentState(GameStates state) {

            if (state == GameStates.Playing)
                StartGame();

            if (state == GameStates.Pause)
                PauseGame();

            if (state == GameStates.FinishGame)
                FinishGame();

            CurrentState = state;
        }

        private void StartGame() {

            if (CurrentState != GameStates.WaitingToStart)
                return;

            foreach (var iListener in _startListeners) {
                iListener.OnStartGame();
            }

            _characterDestroyAction = _context.GetCharacter().Entity.GetIsDestroy();
            _characterDestroyAction.Subscribe(OnCharacterDestroyed);
        }

        private void PauseGame() {

            foreach (var iListener in _pauseListeners) {
                iListener.OnPauseGame();
            }
        }

        private void FinishGame() {

            foreach (var iListener in _finishListeners) {
                iListener.OnFinishGame();
            }
        }

        private void OnCharacterDestroyed(IEntity character) {
            _gameStateChangeAction?.Invoke(GameStates.FinishGame);
        }

        public void Dispose(IContext context) {
            _gameStateChangeAction.Unsubscribe(SetCurrentState);

            if (_characterDestroyAction != null)
                _characterDestroyAction.Unsubscribe(OnCharacterDestroyed);
        }
    }
}
