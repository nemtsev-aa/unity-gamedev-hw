using Atomic.Elements;
using Atomic.Entities;

namespace AtomicFramework.Effects {
    public sealed class SpeedBoostEffect : EffectBase {

        private ReactiveVariable<float> _originalSpeed;

        public SpeedBoostEffect(float duration) {
            Duration = duration;
            Type = EffectType.SpeedBoost;
        }

        public override void Apply(IEntity entity) {
            _originalSpeed = entity.GetMoveSpeed();

            entity.SetMoveSpeed(_originalSpeed.Value * 2f);
            entity.GetSpeedBoostAction().Invoke(true);
        }

        public override void Remove(IEntity entity) {
            entity.SetMoveSpeed(_originalSpeed);
            entity.GetSpeedBoostAction().Invoke(false);
        }
    }
}