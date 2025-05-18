using Atomic.Elements;
using AtomicFramework.Effects;

namespace ZombieShooter.UI {
    public sealed class EffectViewModel {

        public EffectViewModel(EffectType type, ReactiveVariable<float> duration) {
            Type = type;
            Countdown = new Countdown(duration.Value);
        }

        public EffectType Type { get; private set; }
        public Countdown Countdown { get; private set; }
    }
}
