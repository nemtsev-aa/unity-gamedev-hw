using System;
using UnityEngine;
using Atomic.Entities;
using Atomic.Elements;

namespace AtomicFramework.AttackCompanent {

    [Serializable]
    public class MeleeAttackInstaller : IEntityInstaller {
        [SerializeField] private int _damage;
        [SerializeField] private float _range;
        [SerializeField] private float _countdown;

        public void Install(IEntity entity) {
            entity.AddDamage(_damage);
            entity.AddRange(_range);
            entity.AddDelay(_countdown);
            entity.AddIsWithinReach(new ReactiveVariable<bool>(false));

            entity.AddAttackRequest(new BaseEvent());
            entity.AddAttackAction(new BaseEvent());
            entity.AddAttackTerminate(new BaseEvent());

            entity.AddTargetTransform(new ReactiveVariable<Transform>());
            entity.AddTargetChanged(new BaseEvent<Transform>());

            entity.AddBehaviour(new MeleeAttackBehaviour());
        }

        public void SetConfig(AttackConfig attack) {
            SetDamage(attack.Damage);
            SetAttackRange(attack.Range);
            SetAttackCountdown(attack.Delay);
        }

        private void SetDamage(int damage) {
            _damage = damage;
        }

        private void SetAttackRange(float range) {
            _range = range;
        }

        private void SetAttackCountdown(float countdown) {
            _countdown = countdown;
        }
    }
}
