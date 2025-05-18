using Atomic.Elements;

namespace AtomicFramework.AttackCompanent {

    public sealed class MeleeAttackCondition : IValue<bool> {
        private IValue<bool> _attackCondition;
        private ReactiveVariable<bool> _isMoving;

        public MeleeAttackCondition(IValue<bool> attackCondition, ReactiveVariable<bool> isMoving) {
            _attackCondition = attackCondition;
            _isMoving = isMoving;
        }

        public bool Value {
            get {
                return _attackCondition.Value == true
                    && _isMoving.Value == false;
            }
        }
    }
}