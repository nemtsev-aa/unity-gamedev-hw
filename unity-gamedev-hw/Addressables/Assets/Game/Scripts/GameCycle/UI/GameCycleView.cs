using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using R3;

namespace GameCycleSystem {

    public sealed class GameCycleView : MonoBehaviour, IDisposable {
        public Observable<Unit> LoginButtonClick => _loginButton.OnClickAsObservable();
        public Observable<Unit> LogoutButtonClick => _logoutButton.OnClickAsObservable();
        public Observable<Unit> PauseButtonClick => _pauseButton.OnClickAsObservable();

        [SerializeField] private TMP_Text _currentStateText;
        [SerializeField] private Button _loginButton;
        [SerializeField] private Button _logoutButton;
        [SerializeField] private Button _pauseButton;

        private IGameCycleViewModel _viewModel;
        private GameCycle _gameCycle;
        private CompositeDisposable _disposables = new();

        public void Init(IGameCycleViewModel viewModel) {
            _viewModel = viewModel;
            _gameCycle = _viewModel.GameCycle;

            CreateReactiveSubscribes();
        }

        public void Show(bool status) {
            gameObject.SetActive(status);

            if (status == true)
                ShowCurrentGameState();
        }

        private void CreateReactiveSubscribes() {

            LoginButtonClick
                .Subscribe(OnLoginButtonClick)
                .AddTo(_disposables);

            LogoutButtonClick
                .Subscribe(OnLogoutButtonClick)
                .AddTo(_disposables);

            PauseButtonClick
                .Subscribe(OnPauseButtonClick)
                .AddTo(_disposables);
        }

        private void OnLoginButtonClick(Unit unit) {
            _gameCycle.StartGame();
            ShowCurrentGameState();
        }

        private void OnLogoutButtonClick(Unit unit) {
            _gameCycle.FinishGame();
            ShowCurrentGameState();
        }

        private void OnPauseButtonClick(Unit unit) {
            _gameCycle.PauseGame();
            ShowCurrentGameState();
        }

        private void ShowCurrentGameState() {
            var currentState = _gameCycle.CurrentState;

            if (currentState == GameStates.WaitingToStart) {
                _currentStateText.text = $"Game State: Waiting To Start";
                return;
            }

            _currentStateText.text = $"Game State: {currentState}";
        }

        public void Dispose() {

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();
        }
    }
}



