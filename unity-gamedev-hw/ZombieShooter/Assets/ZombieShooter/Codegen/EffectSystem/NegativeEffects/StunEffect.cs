using Atomic.Elements;
using Atomic.Entities;

namespace AtomicFramework.Effects {
    
    public sealed class StunEffect : EffectBase {

        private IValue<bool> _originalCanMove;
        private IValue<bool> _originalCanRotate;
        private IValue<bool> _originalCanAttack;

        public StunEffect(float duration) {
            Duration = duration;
            Type = EffectType.Stun;
        }

        public override void Apply(IEntity entity) {
            _originalCanMove = entity.GetCanMove();
            _originalCanRotate = entity.GetCanRotate();
            _originalCanAttack = entity.GetCanAttack();

            entity.SetCanMove(new ReactiveBool(false));
            entity.SetCanRotate(new ReactiveBool(false));
            entity.SetCanAttack(new ReactiveBool(false));

            entity.GetStunAction().Invoke(true);
        }

        public override void Remove(IEntity entity) {
            entity.SetCanMove(_originalCanMove);
            entity.SetCanRotate(_originalCanRotate);
            entity.SetCanAttack(_originalCanAttack);

            entity.GetStunAction().Invoke(false);
        }
    }
}