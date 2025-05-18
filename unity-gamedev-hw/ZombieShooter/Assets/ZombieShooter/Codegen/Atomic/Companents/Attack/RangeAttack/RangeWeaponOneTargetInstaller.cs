using UnityEngine;
using Atomic.Entities;
using ZombieShooter.SceneObjects;

namespace AtomicFramework.AttackCompanent {

    public class RangeWeaponOneTargetInstaller : RangeWeaponInstaller {
        [SerializeField] private Transform _target;

        public override void Install(IEntity entity) {
            base.Install(entity);

            if (_target != null)
                entity.AddTargetTransform(_target);
        }

        public override void SetWeaponConfig(WeaponConfig config) {
            base.SetWeaponConfig(config);

            SetTarget(config.Target);
        }

        private void SetTarget(Transform target) {
            _target = target;
        }
    }
}