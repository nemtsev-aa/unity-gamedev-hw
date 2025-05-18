using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;
using AtomicFramework.Contextes;
using AtomicFramework.EntityPool;
using System;
using UnityEngine;
using ZombieShooter.SceneObjects;

namespace AtomicFramework.BulletSystem {

    [Serializable]
    public sealed class BulletSystemInstaller : IContextInstaller {
        [SerializeField] private BulletSystemConfig _systemConfig;
        [SerializeField] private BulletConfig _config;

        public void Install(IContext context) {
            context.AddBulletSystemConfig(_systemConfig);
            context.AddBulletConfig(_config);

            var systemData = CreateBulletSystemData(context);

            context.AddBulletSystemData(systemData);
            context.AddSystem<BulletSpawnSystem>();
        }

        private BulletSystemData CreateBulletSystemData(IContext context) {
            var bulletEntity = _systemConfig.Prefab.Entity;
            var weaponEntity = context.GetWeapon();
            var firePoint = weaponEntity.GetFirePoint();

            var containersPresenter = GameContext.Instance.GetContainersPresenter();
            var poolContainer = containersPresenter.BulletContainer;
            var worldContainer = containersPresenter.WorldContainer;
            var spawnCycle = new Cycle(_systemConfig.SpawnPeriod);

            var pool = new SceneEntityPool(bulletEntity, poolContainer, worldContainer, _systemConfig.InitialPoolCount);

            return new BulletSystemData(pool, _config, firePoint, spawnCycle);
        }
    }
}
