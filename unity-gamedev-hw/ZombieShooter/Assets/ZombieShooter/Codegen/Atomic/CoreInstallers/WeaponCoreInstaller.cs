using UnityEngine;
using Atomic.Entities;
using ZombieShooter.SceneObjects;
using AtomicFramework.AttackCompanent;
using AtomicFramework.BulletSystem;
using AtomicFramework.Contextes;

namespace AtomicFramework.CoreInstallers {

    public class WeaponCoreInstaller : SceneEntityInstallerBase {
        [SerializeField] private BulletSystemInstaller _bulletSystemInstaller;
        [SerializeField] private RangeWeaponInstaller _rangeWeaponInstaller;
        [Space(10)]
        [SerializeField] private bool _showLogAfterExecution;

        private WeaponConfig _weaponConfig;
        private GameContext _context;

        public override void Install(IEntity entity) {
            _context = GameContext.Instance;

            _rangeWeaponInstaller.Install(entity);
            _bulletSystemInstaller.Install(_context);

            var weaponController = new WeaponController();
            weaponController.Install(_context);

            if (_showLogAfterExecution == true)
                Debug.Log($"{nameof(WeaponCoreInstaller)} installed");
        }

        public void SetConfig(WeaponConfig weaponConfig) {
            _weaponConfig = weaponConfig;
            _rangeWeaponInstaller.SetWeaponConfig(_weaponConfig);
        }
    }
}
