namespace GameCycleSystem {
    public sealed class GameCycleViewModel : IGameCycleViewModel {
        public GameCycle GameCycle { get; }

        public GameCycleViewModel(GameCycle gameCycle) {
            GameCycle = gameCycle;
        }
    }
}



