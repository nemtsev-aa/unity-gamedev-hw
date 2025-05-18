using Atomic.Entities;
using AtomicFramework.BulletSystem;
using AtomicFramework.CoreInstallers;
using UnityEngine;

namespace ZombieShooter.SceneObjects {

    public class Weapon : SceneObject {
        [field: SerializeField] public FirePoint FirePoint { get; private set; }

        private WeaponConfig _weaponConfig;

        public void Init(WeaponConfig weaponConfig) {
            _weaponConfig = weaponConfig;

            if (transform.TryGetComponent(out WeaponCoreInstaller core) == true) {
                core.SetConfig(_weaponConfig);
                core.Install(Entity);
            }
        }

        public void SetFirePoint(FirePoint firePoint) {
            Entity.AddFirePoint(firePoint);
        }
    }
}
