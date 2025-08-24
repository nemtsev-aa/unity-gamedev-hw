using StepByStepButler.Gameplay;

namespace EventBusService {
    public struct GameOverEvent : IEvent {
        public PlayerType Winner { get; }
        public GameOverEvent(PlayerType winner) => Winner = winner;
    }
}
