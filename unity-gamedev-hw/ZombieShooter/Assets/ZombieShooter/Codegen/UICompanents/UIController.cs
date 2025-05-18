using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;
using AtomicFramework.EnemySystem;
using System;
using UnityEngine;
using ZombieShooter.GameCycleSystem;

namespace ZombieShooter.UI {

    [Serializable]
    public class UIController : IContextInit,
                                IGameStartListener,
                                IGameFinishListener {

        [SerializeField] private InitializeGamePopup _initializeGamePopup;
        [SerializeField] private StartGamePopup _startGamePopup;
        [SerializeField] private GameplayPopup _gameplayPopup;
        [SerializeField] private EndGamePopup _endGamePopup;

        private IContext _gameContext;
        private SceneEntity _characterEntity;
        private SceneEntity _weaponEntity;
        private EnemyManager _enemyManager;

        private UIPopup _currentPopup;
        private IEvent<GameStates> _gameStateChangeAction;
        private IEvent _characterCreateEvent;

        public void Init(IContext context) {
            _gameContext = context;

            _gameStateChangeAction = _gameContext.GetGameStateChangeAction();
            _gameStateChangeAction.Subscribe(SwitchCurrentPopup);

            InitPopups();
            SwitchCurrentPopup(_gameContext.GetSystem<GameCycle>().CurrentState);
        }

        public void OnStartGame() {

            if (CheckCompanents() == false)
                return;

            _characterCreateEvent = _gameContext.GetCharactreCreaetEvent();
            _characterCreateEvent.Subscribe(OnCharacterCreated);
        }

        public void OnFinishGame() {
            _characterEntity = null;
            _weaponEntity = null;
        }

        private void OnCharacterCreated() {
            _characterEntity = _gameContext.GetCharacter();
            _weaponEntity = _gameContext.GetWeapon();
            _enemyManager = _gameContext.GetSystem<EnemyManager>();

            var gameplayPopupViewModel = CreateGameplayPopupViewModel(_characterEntity, _weaponEntity, _enemyManager);
            _gameplayPopup.Init(gameplayPopupViewModel);
        }

        private void InitPopups() {
            var startGamePopupViewModel = CreateStartGamePopupViewModel(_gameContext);
            _startGamePopup.Init(startGamePopupViewModel);

            var endGamePopupViewModel = CreateEndGamePopupViewModel(_gameContext);
            _endGamePopup.Init(endGamePopupViewModel);
        }

        private void SwitchCurrentPopup(GameStates gameState) {

            if (_currentPopup != null)
                _currentPopup.Show(false);

            switch (gameState) {
                case GameStates.InitializingComponents:
                    _currentPopup = _initializeGamePopup;
                    break;

                case GameStates.WaitingToStart:
                    _currentPopup = _startGamePopup;
                    break;

                case GameStates.Playing:
                    _currentPopup = _gameplayPopup;
                    break;

                case GameStates.Pause:
                    _currentPopup = _startGamePopup;
                    break;

                case GameStates.FinishGame:
                    _currentPopup = _endGamePopup;
                    break;

                default:
                    break;
            }

            if (_currentPopup != null)
                _currentPopup.Show(true);
        }

        #region CreateViewModels

        private StartGamePopupViewModel CreateStartGamePopupViewModel(IContext gameContext) {
            return new StartGamePopupViewModel(gameContext);
        }

        private GameplayPopupViewModel CreateGameplayPopupViewModel(
                                           SceneEntity characterEntity,
                                           SceneEntity weaponEntity,
                                           EnemyManager enemyManager) {

            var hitPoints = characterEntity.GetHitPoints();
            var bulletAmount = weaponEntity.GetCurrentBulletAmount();
            var maxBulletAmount = weaponEntity.GetMaxBulletAmount();
            var killEnemyCount = enemyManager.KillEnemyCount;

            return new GameplayPopupViewModel(hitPoints, bulletAmount, maxBulletAmount, killEnemyCount);
        }

        private EndGamePopupViewModel CreateEndGamePopupViewModel(IContext gameContext) {
            return new EndGamePopupViewModel(gameContext);
        }

        #endregion

        #region Validate

        private bool CheckCompanents() {

            if (_startGamePopup == null)
                throw new ArgumentNullException($"{nameof(StartGamePopup)} not found!");

            if (_gameplayPopup == null)
                throw new ArgumentNullException($"{nameof(GameplayPopup)} not found!");

            if (_endGamePopup == null)
                throw new ArgumentNullException($"{nameof(EndGamePopup)} not found!");

            return true;
        }

        #endregion
    }
}

