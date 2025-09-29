using R3;
using System;
using UnityEngine;
using GameCycleSystem;
using Client.Components.Teams;
using Client.Components.Common;
using Zenject;

namespace UICompanents {

    public class UIManager : MonoBehaviour, IDisposable {
        public event Action StartButtonClicked;
        public event Action PauseButtonClicked;
        public event Action FinishButtonClicked;
        public event Action<TeamTypes, UnitTypes> UnitCreatedRequest;

        public GameCycleView GameCycleView => _gameCycleView;

        [SerializeField] private GameCycleView _gameCycleView;
        [SerializeField] private WinnerView _winnerView;
        [SerializeField] private TeamPanel _blueTeamPanel;
        [SerializeField] private TeamPanel _redTeamPanel;

        private GameMediator _gameMediator;
        private CompositeDisposable _disposables = new();

        [Inject]
        public void Construct(GameMediator gameMediator) {
            _gameMediator = gameMediator;
        }

        public void Init() {
            InitPanels();
            ShowDefaultState();
        }

        private void InitPanels() {
            var viewModel = new WinnerViewModel(_gameMediator);
            _winnerView.Init(viewModel);

            _blueTeamPanel.Init(TeamTypes.Blue);
            _blueTeamPanel.CreationAction
                .Subscribe(BlueTeam_CreationAction)
                .AddTo(_disposables);

            _redTeamPanel.Init(TeamTypes.Red);
            _redTeamPanel.CreationAction
               .Subscribe(ReadTeam_CreationAction)
               .AddTo(_disposables);
        }

        private void BlueTeam_CreationAction(UnitTypes types) {
            UnitCreatedRequest?.Invoke(TeamTypes.Blue, types);
        }

        private void ReadTeam_CreationAction(UnitTypes types) {
            UnitCreatedRequest?.Invoke(TeamTypes.Red, types);
        }

        public void ShowDefaultState() {
            _blueTeamPanel.Show(false);
            _redTeamPanel.Show(false);

            _gameCycleView.ShowDefaultState();
        }

        public void ShowGameplayState() {
            StartButtonClicked?.Invoke();

            _gameCycleView.ShowGameplayState();
            _blueTeamPanel.Show(true);
            _redTeamPanel.Show(true);
        }

        public void ShowPauseState() {
            _gameCycleView.ShowPauseState();

            _blueTeamPanel.Show(false);
            _redTeamPanel.Show(false);
        }

        private void PauseButtonClick() {
            PauseButtonClicked?.Invoke();
        }

        private void FinishButtonClick() {
            FinishButtonClicked?.Invoke();
            ShowDefaultState();
        }

        public void Dispose() {
            //RemoveListeners();
            ShowDefaultState();

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();
        }
    }
}