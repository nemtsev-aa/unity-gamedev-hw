using Atomic.Elements;

namespace AtomicFramework.ShootCompanent {

    public sealed class ShootCondition : IValue<bool> {
        private IValue<bool> _attackCondition;
        private ReactiveVariable<int> _charges;

        public ShootCondition(IValue<bool> attackCondition, ReactiveVariable<int> charges) {
            _attackCondition = attackCondition;
            _charges = charges;
        }

        public bool Value {
            get {
                return _attackCondition.Value == true
                    && _charges.Value > 0;
            }
        }
    }
}
