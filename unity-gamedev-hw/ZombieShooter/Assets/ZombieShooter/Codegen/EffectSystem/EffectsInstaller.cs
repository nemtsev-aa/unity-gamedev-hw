using System;
using Atomic.Elements;
using Atomic.Entities;

namespace AtomicFramework.Effects {

    [Serializable]
    public sealed class EffectsInstaller : IEntityInstaller {

        public void Install(IEntity entity) {
            entity.AddStunAction(new BaseEvent<bool>());
            entity.AddSpeedBoostAction(new BaseEvent<bool>());

            entity.AddBehaviour(new EffectsSystem());
        }
    }
}