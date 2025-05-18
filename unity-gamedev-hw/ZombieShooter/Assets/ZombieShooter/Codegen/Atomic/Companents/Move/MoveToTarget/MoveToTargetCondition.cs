using Atomic.Elements;

namespace AtomicFramework.MoveCompanent {

    public sealed class MoveToTargetCondition : IValue<bool> {

        private IValue<bool> _movingCondition;
        private ReactiveVariable<bool> _isTargetDead;

        public MoveToTargetCondition(IValue<bool> movingCondition,
                            ReactiveVariable<bool> isTargetDead) {

            _movingCondition = movingCondition;
            _isTargetDead = isTargetDead;
        }

        public bool Value {
            get {
                return _movingCondition.Value == true
                    && _isTargetDead.Value == false;
            }
        }
    }
}
