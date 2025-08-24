namespace EventBusService {
    public struct FreezeEvent : IEvent {
        public int TargetId { get; }
        public FreezeEvent(int targetId) => TargetId = targetId;
    }
}
