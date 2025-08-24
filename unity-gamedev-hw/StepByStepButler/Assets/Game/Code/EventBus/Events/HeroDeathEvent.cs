namespace EventBusService {

    public struct HeroDeathEvent : IEvent {
        public int HeroId { get; }
        
        public HeroDeathEvent(int heroId) => HeroId = heroId;
    }
}
