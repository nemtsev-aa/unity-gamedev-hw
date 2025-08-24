namespace EventBusService {
    
    public struct TurnStartedEvent : IEvent {
        public int HeroId { get; }
        
        public TurnStartedEvent(int heroId) => HeroId = heroId; 
    }
}
