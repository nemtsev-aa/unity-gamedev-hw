namespace EventBusService {
    
    public struct HealthChangedEvent : IEvent {
        public int HeroId { get; }
        public int NewHealth { get; }
        public int MaxHealth { get; }

        public HealthChangedEvent(int heroId, int newHealth, int maxHealth) {
            HeroId = heroId;
            NewHealth = newHealth;
            MaxHealth = maxHealth;
        }
    }
}
