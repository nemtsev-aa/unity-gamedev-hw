using System;
using UnityEngine;
using Atomic.Elements;
using Atomic.Entities;
using AtomicFramework.ZombieShooter;

namespace AtomicFramework.LifeCompanent {

    [Serializable]
    public sealed class LifeInstaller : IEntityInstaller {
        [SerializeField] private float _hitPoints = 10f;
        
        private LifeConfig _config;

        public void Install(IEntity entity) {
            entity.AddHitPoints(new ReactiveVariable<float>(_hitPoints));
            entity.AddIsDeath(new ReactiveVariable<bool>(false));
            entity.AddIsDestroy(new BaseEvent<IEntity>());
            entity.AddTakeDamageAction(new BaseEvent<float>());

            entity.AddBehaviour(new LifeBehaviour());
        }

        public void SetConfig(LifeConfig config) {
            _config = config;

            SetHitPoints(_config.HitPointCount);
        }

        public void SetHitPoints(float hitPoints) {
            _hitPoints = hitPoints;
        }
    }
}

