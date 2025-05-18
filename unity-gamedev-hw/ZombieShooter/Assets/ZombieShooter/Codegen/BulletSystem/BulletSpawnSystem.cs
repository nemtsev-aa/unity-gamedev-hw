using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;
using AtomicFramework.EntityPool;
using System;

namespace AtomicFramework.BulletSystem {

    public sealed class BulletSpawnSystem : IContextInit {
        private IEntityPool _pool;
        private BulletConfig _config;
        private IEvent _attackAction;
        private BaseEvent<IEntity> _isDestroy;
        private IEvent<Bullet> _attackEvent;

        public void Init(IContext context) {
            var bulletSystemData = context.GetBulletSystemData();

            _pool = bulletSystemData.Pool;
            _config = bulletSystemData.Config;

            var weapon = context.GetWeapon();
            _attackAction = weapon.GetAttackAction();
            _attackAction.Subscribe(OnAttackAction);

            _attackEvent = weapon.GetAttackEvent();
        }

        private void OnAttackAction() {
            Bullet activeBullet = RentBullet();
             _attackEvent?.Invoke(activeBullet);
        }

        private Bullet RentBullet() {
            var iEntity = _pool.Rent();

            if (SceneEntity.TryCast(iEntity, out SceneEntity entity) == true) {

                if (entity.TryGetComponent(out Bullet bullet) == true) {
                    
                    if (bullet.IsInstall == true)
                        return bullet;

                    if (bullet.TryGetComponent(out BulletCoreInstaller coreInstaller) == true) {
                        coreInstaller.SetConfig(_config);
                        coreInstaller.Install(entity);
                        bullet.SetInstall(true);

                        _isDestroy = entity.GetIsDestroy();
                        _isDestroy.Subscribe(OnBulletDestroy);

                        return bullet;
                    }

                    throw new ArgumentException($"Bullet not contain BulletCoreInstaller!");
                }

                throw new ArgumentException($"Entity not contain Bullet!");
            }

            throw new ArgumentException($"PoolObject not contain SceneEntity!");
        }

        private void OnBulletDestroy(IEntity bullet) {
            _pool.Return(bullet);
        }
    }
}
