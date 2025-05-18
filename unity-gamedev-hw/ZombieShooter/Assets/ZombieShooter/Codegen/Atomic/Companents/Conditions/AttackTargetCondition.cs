using Atomic.Elements;

namespace AtomicFramework.Conditions {

    public sealed class AttackTargetCondition : IValue<bool> {

        private IValue<bool> _isAttack;
        private ReactiveVariable<bool> _isTargetDead;

        public AttackTargetCondition(IValue<bool> isAttack, ReactiveVariable<bool> isTargetDead) {
            _isAttack = isAttack;
            _isTargetDead = isTargetDead;
        }

        public bool Value {
            get {
                return _isAttack.Value == true
                    && _isTargetDead.Value == false;
            }
        }
    }
}
