namespace EventBusService {
    
    public struct DamageEvent : IEvent {
        public int TargetId { get; }
        public int Damage { get; }
        public int AttackerId { get; }

        public DamageEvent(int targetId, int damage, int attackerId = -1) {
            TargetId = targetId;
            Damage = damage;
            AttackerId = attackerId;
        }
    }
}
