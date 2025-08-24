namespace EventBusService {

    public struct AbilityUsedEvent : IEvent {
        public int HeroId { get; }

        public AbilityUsedEvent(int heroId) => HeroId = heroId;
    }
}
