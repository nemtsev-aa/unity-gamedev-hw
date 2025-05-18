namespace ZombieShooter.GameCycleSystem {

    public interface IGamePauseListener : IGameListener {
        void OnPauseGame();
    }
}

