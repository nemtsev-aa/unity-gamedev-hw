using Atomic.Contexts;
using Atomic.Elements;
using ZombieShooter.GameCycleSystem;

namespace ZombieShooter.UI {

    public sealed class EndGamePopupViewModel {
        private readonly IContext _gameContext;

        public EndGamePopupViewModel(IContext gameContext) {
            _gameContext = gameContext;

            GameStateChangeAction = _gameContext.GetGameStateChangeAction();
        }

        public IEvent<GameStates> GameStateChangeAction;
    }
}
