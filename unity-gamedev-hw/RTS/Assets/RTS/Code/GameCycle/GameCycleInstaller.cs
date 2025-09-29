using Client.Services;

namespace GameCycleSystem {

    public sealed class GameCycleInstaller {

        private readonly GameCycle _gameCycle;
        private readonly EcsStartup _ecsStartup;
        private readonly UnitManager _unitManager;
        private readonly GameMediator _gameMediator;
        private readonly TownHallDestroyObserver _destroyObserver;

        public GameCycleInstaller(GameCycle gameCycle,
                                  EcsStartup ecsStartup,
                                  GameMediator gameMediator,
                                  UnitManager unitManager,
                                  TownHallDestroyObserver destroyObserver) {

            _gameCycle = gameCycle;
            _ecsStartup = ecsStartup;
            _gameMediator = gameMediator;
            _unitManager = unitManager;
            _destroyObserver = destroyObserver;

            AddGameListeners();
        }

        private void AddGameListeners() {
            _gameCycle.Add(_ecsStartup);
            _gameCycle.Add(_gameMediator);
            _gameCycle.Add(_unitManager);
            _gameCycle.Add(_destroyObserver);
        }
    }
}