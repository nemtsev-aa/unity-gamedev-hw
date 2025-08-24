using UI;
using StepByStepButler.Gameplay.Heroes;

namespace EventBusService {

    public struct HeroDeathAnimationEvent : IEvent {
        public int HeroId { get; }
        public HeroView HeroView { get; }
        public HeroType HeroType { get; }

        public HeroDeathAnimationEvent(int heroId, HeroView heroView, HeroType heroType) {
            HeroId = heroId;
            HeroView = heroView;
            HeroType = heroType;
        }
    }
}
