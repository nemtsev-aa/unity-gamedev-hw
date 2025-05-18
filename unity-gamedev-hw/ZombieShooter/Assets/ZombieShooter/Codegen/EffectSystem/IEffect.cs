using Atomic.Entities;

namespace AtomicFramework.Effects {

    public interface IEffect {
        EffectType Type { get; set; }
        float Duration { get; set; }

        void Apply(IEntity entity);
        void Remove(IEntity entity);
    }
}