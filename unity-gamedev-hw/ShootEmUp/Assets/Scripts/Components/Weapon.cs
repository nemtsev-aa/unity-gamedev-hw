using UnityEngine;

namespace ShootEmUp {
    public sealed class Weapon {
        private Transform _firePoint;

        public Weapon(Transform firePoint) {
            _firePoint = firePoint;
        }

        public Vector2 Position => _firePoint.position;
        public Quaternion Rotation => _firePoint.rotation;

    }
}