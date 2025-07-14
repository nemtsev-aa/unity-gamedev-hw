namespace GameCycleSystem {

    public interface IGameLateUpdateListener : IGameListener {
        void OnLateUpdateGame();
    }
}

