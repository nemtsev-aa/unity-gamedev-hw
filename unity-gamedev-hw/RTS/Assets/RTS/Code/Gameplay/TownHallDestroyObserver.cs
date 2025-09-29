using R3;
using System;
using Client.Components.Teams;
using Unit = R3.Unit;

namespace GameCycleSystem {

    public sealed class TownHallDestroyObserver : IGameStartListener, IDisposable {
        private readonly GameMediator _gameMediator;
        private readonly TownHallProvider _provider;

        private readonly CompositeDisposable _disposables = new();

        public TownHallDestroyObserver(GameMediator gameMediator,
                                       TownHallProvider provider) {

            _gameMediator = gameMediator;
            _provider = provider;
        }

        public void OnStartGame() {

            _provider.Blue.IsDestroyed
                .Subscribe(BlueTeamTownHall_IsDestroyed)
                .AddTo(_disposables);

            _provider.Red.IsDestroyed
                .Subscribe(RedTeamTownHall_IsDestroyed)
                .AddTo(_disposables);
        }

        private void BlueTeamTownHall_IsDestroyed(Unit unit) =>
            _gameMediator.SetWinner(TeamTypes.Red);


        private void RedTeamTownHall_IsDestroyed(Unit unit) =>
            _gameMediator.SetWinner(TeamTypes.Blue);

        public void Dispose() {

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();
        }
    }
}