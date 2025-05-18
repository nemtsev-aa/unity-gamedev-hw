using Atomic.Elements;

namespace AtomicFramework.Conditions {

    public sealed class MoveCondition : IValue<bool> {

        private ReactiveVariable<bool> _isDead;
        private ReactiveVariable<float> _speed;
        private ReactiveVariable<bool> _isMoving;

        public MoveCondition(ReactiveVariable<bool> isDead,
                            ReactiveVariable<float> speed,
                            ReactiveVariable<bool> isMoving) {

            _isDead = isDead;
            _speed = speed;
            _isMoving = isMoving;
        }

        public bool Value {
            get {
                return _isDead.Value == false
                    && _speed.Value > 0
                    && _isMoving.Value == true;
            }
        }
    }
}
