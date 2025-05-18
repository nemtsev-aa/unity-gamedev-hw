using Atomic.Elements;
using Atomic.Entities;
using AtomicFramework.BulletSystem;
using AtomicFramework.MoveCompanent;
using System;
using ZombieShooter.SceneObjects;

namespace AtomicFramework.AttackCompanent {

    [Serializable]
    public class RangeWeaponInstaller : IEntityInstaller {
        private Bullet _bulletPrefab;
        private float _reloadTime;
        private int _maxBulletAmount;
        private float _addBulletDelay;

        private WeaponConfig _config;

        public virtual void Install(IEntity entity) {
            entity.AddAttackRequest(new BaseEvent());
            entity.AddAttackAction(new BaseEvent());
            entity.AddAttackEvent(new BaseEvent<Bullet>());

            entity.AddCurrentBulletAmount(_maxBulletAmount);
            entity.AddMaxBulletAmount(_maxBulletAmount);
            entity.AddIsWithinReach(new ReactiveVariable<bool>(true));

            var assaultRifleBehaviour = new AssaultRifleBehaviour(_addBulletDelay, _reloadTime);
            entity.AddBehaviour(assaultRifleBehaviour);

            assaultRifleBehaviour.Init(entity);
        }

        public virtual void SetWeaponConfig(WeaponConfig config) {
            _config = config;

            SetBulletPrefab(_config.BulletPrefab);
            SetMaxBulletAmount(_config.MaxBulletAmount);
            SetAddBulletDelay(_config.AddBulletDelay);
            SetReloadTime(_config.ReloadTime);
        }

        private void SetBulletPrefab(Bullet bulletPrefab) {
            _bulletPrefab = bulletPrefab;
        }

        private void SetReloadTime(float reloadTime) {
            _reloadTime = reloadTime;
        }

        private void SetMaxBulletAmount(int maxBulletAmount) {
            _maxBulletAmount = maxBulletAmount;
        }

        private void SetAddBulletDelay(float addBulletDelay) {
            _addBulletDelay = addBulletDelay;
        }
    }
}