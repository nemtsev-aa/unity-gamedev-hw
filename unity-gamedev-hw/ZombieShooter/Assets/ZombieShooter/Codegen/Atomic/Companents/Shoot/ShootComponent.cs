using System;
using UnityEngine;
using Atomic.Entities;
using AtomicFramework.BulletSystem;
using Object = UnityEngine.Object;

namespace AtomicFramework.ShootCompanent {

    [Serializable]
    public class ShootComponent {

        private readonly Bullet _bulletPrefab;
        private readonly Transform _firePoint;

        public ShootComponent(Bullet bulletPrefab, Transform firePoint) {
            _bulletPrefab = bulletPrefab;
            _firePoint = firePoint;
        }

        public void Shoot() {
            var bullet = Object.Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
            bullet.Entity.SetMoveDirection(_firePoint.forward);
        }
    }
}
