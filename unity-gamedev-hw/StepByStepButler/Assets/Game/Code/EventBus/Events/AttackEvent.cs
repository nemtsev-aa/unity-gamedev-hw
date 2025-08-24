namespace EventBusService {
    
    public struct AttackEvent : IEvent {
        public int AttackerId { get; }
        public int TargetId { get; }
        
        public AttackEvent(int attackerId, int targetId) {
            AttackerId = attackerId;
            TargetId = targetId;
        }
    }
}
