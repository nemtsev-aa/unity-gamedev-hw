using Atomic.Elements;

namespace AtomicFramework.Conditions {

    public class AttackCondition : IValue<bool> {

        private ReactiveVariable<bool> _isWithinReach;
        private ReactiveVariable<bool> _isReloading;

        public AttackCondition(ReactiveVariable<bool> isWithinReach,
                            ReactiveVariable<bool> isReloading) {

            _isWithinReach = isWithinReach;
            _isReloading = isReloading;
        }

        public bool Value {
            get {
                return _isWithinReach.Value == true
                    && _isReloading.Value == false;
            }
        }
    }
}
