using BehaviorTree.PlayerVisualSubSystem;
using System;

namespace BehaviorTree.PlayerCompanents {
    public sealed class AttackAnimationHandler : IDisposable {
        public event Action<bool> AnimationStarted;
        public event Action AttackEventActivated;

        private const string START = "FarmingStart";
        private const string APPLAY_DAMAGE = "ApplyDamage";
        private const string END = "FarmingEnd";

        private readonly AnimationDispatcher _dispatcher;

        public AttackAnimationHandler(AnimationDispatcher dispatcher) {
            _dispatcher = dispatcher;
            _dispatcher.EventReceived += OnEventReceived;
        }

        private void OnEventReceived(string eventName) {

            switch (eventName) {
                case START:
                    AnimationStarted?.Invoke(true);
                    break;

                case APPLAY_DAMAGE:
                    AttackEventActivated?.Invoke();
                    break;

                case END:
                    AnimationStarted?.Invoke(false);
                    break;

                default:
                    break;
            }
        }

        public void Dispose() {
            _dispatcher.EventReceived -= OnEventReceived;
        }
    }
}



