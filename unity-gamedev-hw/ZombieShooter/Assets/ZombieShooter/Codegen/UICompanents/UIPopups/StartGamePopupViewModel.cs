using Atomic.Contexts;
using Atomic.Elements;
using ZombieShooter.GameCycleSystem;

namespace ZombieShooter.UI {
    public sealed class StartGamePopupViewModel {
        private readonly IContext _gameContext;

        public StartGamePopupViewModel(IContext gameContext) {
            _gameContext = gameContext;

            GameStateChangeAction = _gameContext.GetGameStateChangeAction();
        }

        public IEvent<GameStates> GameStateChangeAction;
    }
}

