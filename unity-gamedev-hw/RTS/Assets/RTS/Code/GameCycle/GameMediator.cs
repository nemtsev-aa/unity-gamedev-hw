using R3;
using System;
using UICompanents;
using Client.Components.Teams;
using Client.Components.Common;
using UnityEngine.SceneManagement;
using Unit = R3.Unit;

namespace GameCycleSystem {

    public sealed class GameMediator : IGameStartListener,
                                       IGamePauseListener,
                                       IGameFinishListener,
                                       IDisposable {
        public Observable<TeamTypes> WinnerTeam => _winnerTeam;

        private GameCycle _gameCycle;
        private UIManager _uIManager;
        private UnitManager _unitManager;

        private Subject<TeamTypes> _winnerTeam = new();
        private CompositeDisposable _disposables = new();

        public GameMediator(GameCycle gameCycle,
                            UIManager uIManager,
                            UnitManager unitManager) {

            _gameCycle = gameCycle;
            _uIManager = uIManager;
            _unitManager = unitManager;

            SubscribeToUIEvents();
        }

        public void Init() {
            _uIManager.Init();
            _gameCycle.SetCurrentState(GameStates.WaitingToStart);
        }

        public void SetWinner(TeamTypes team) {
            _winnerTeam.OnNext(team);
            OnFinishGame();
        }

        public void OnStartGame() {
            _uIManager.ShowGameplayState();
        }

        public void OnPauseGame() {

            if (_gameCycle.CurrentState == GameStates.Playing) {
                _uIManager.ShowPauseState();
                return;
            }

            if (_gameCycle.CurrentState == GameStates.Pause)
                _uIManager.ShowGameplayState();
        }

        public void OnFinishGame() {
            _uIManager.GameCycleView.ShowFinishState();
        }

        private void SubscribeToUIEvents() {

            var panel = _uIManager.GameCycleView;

            panel.StartButtonClicked
                .Subscribe(OnStartGameplay)
                .AddTo(_disposables);

            panel.PauseButtonClicked
               .Subscribe(OnPauseGameplay)
               .AddTo(_disposables);

            panel.FinishButtonClicked
              .Subscribe(OnFinishGameplay)
              .AddTo(_disposables);

            _uIManager.UnitCreatedRequest += OnUnitCreatedRequest;
        }

        private void OnUnitCreatedRequest(TeamTypes team, UnitTypes unit) {
            _unitManager.CreateUnit(team, unit);
        }

        private void OnStartGameplay(Unit _) =>
            _gameCycle.StartGame();

        private void OnPauseGameplay(Unit _) =>
            _gameCycle.PauseGame();

        private void OnFinishGameplay(Unit _) {
            _gameCycle.FinishGame();
            ReloadCurrentScene();
        }

        private void ReloadCurrentScene() {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }

        public void Dispose() {
            OnFinishGame();

            _uIManager.UnitCreatedRequest -= OnUnitCreatedRequest;

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();
        }
    }
}