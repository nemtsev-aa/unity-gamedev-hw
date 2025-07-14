using ChestsSystem;
using SessionTrackerSystem;

namespace GameCycleSystem {

    public class GameCycleInitializer {
        private readonly GameCycle _gameCycle;
        private readonly SessionTracker _sessionTracker;
        private readonly ChestSystem _chestSystem;

        public GameCycleInitializer(GameCycle gameCycle,
                                  SessionTracker sessionTracker,
                                  ChestSystem chestSystem) {

            _gameCycle = gameCycle;
            _sessionTracker = sessionTracker;
            _chestSystem = chestSystem;

            AddGameListeners();
        }

        public void AddGameListeners() {
            _gameCycle.Add(_sessionTracker);
            _gameCycle.Add(_chestSystem);
        }
    }
}


