namespace EventBusService {

    public struct DeathAnimationCompleteEvent : IEvent {
        public int HeroId { get; set; }

        public DeathAnimationCompleteEvent(int heroId) => HeroId = heroId;
    }
}
