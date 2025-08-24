namespace EventBusService {

    public struct HealEvent : IEvent {
        public int TargetId { get; }
        public int Amount { get; }
        public HealEvent(int targetId, int amount) {
            TargetId = targetId;
            Amount = amount;
        }
    }
}
