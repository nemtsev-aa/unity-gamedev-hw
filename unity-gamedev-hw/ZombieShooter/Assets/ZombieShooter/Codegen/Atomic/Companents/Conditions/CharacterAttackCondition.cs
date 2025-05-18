using Atomic.Elements;

namespace AtomicFramework.Conditions {

    public sealed class CharacterAttackCondition : IValue<bool> {
        private ReactiveVariable<bool> _isDeath;

        public CharacterAttackCondition(ReactiveVariable<bool> isDeath) {
            _isDeath = isDeath;
        }

        public bool Value {
            get {
                return _isDeath.Value == false;
            }
        }
    }
}