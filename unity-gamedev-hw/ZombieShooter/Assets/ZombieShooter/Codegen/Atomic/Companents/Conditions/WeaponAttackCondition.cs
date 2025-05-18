using Atomic.Elements;

namespace AtomicFramework.Conditions {

    public sealed class WeaponAttackCondition : IValue<bool> {
        private IValue<bool> _attackCondition;
        private IValue<bool> _userCondition;

        public WeaponAttackCondition(IValue<bool> attackCondition, IValue<bool> userCondition) {
            _attackCondition = attackCondition;
            _userCondition = userCondition;
        }

        public bool Value {
            get {
                return _attackCondition.Value == true
                    && _userCondition.Value == true;
            }
        }
    }
}