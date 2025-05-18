using Atomic.Entities;

namespace AtomicFramework.Effects {

    public abstract class EffectBase : IEffect {
        public EffectType Type { get; set; }
        public float Duration { get; set; }

        public abstract void Apply(IEntity entity);
        public abstract void Remove(IEntity entity);
    }
}