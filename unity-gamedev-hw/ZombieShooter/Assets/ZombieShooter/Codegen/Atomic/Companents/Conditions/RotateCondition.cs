using Atomic.Elements;

namespace AtomicFramework.Conditions {

    public sealed class RotateCondition : IValue<bool> {

        private ReactiveVariable<bool> _isDead;
        private ReactiveVariable<float> _speed;

        public RotateCondition(ReactiveVariable<bool> isDead,
                            ReactiveVariable<float> speed) {

            _isDead = isDead;
            _speed = speed;
        }

        public bool Value {
            get {
                return _isDead.Value == false
                    && _speed.Value > 0;
            }
        }
    }
}
