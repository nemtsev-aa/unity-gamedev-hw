using UnityEngine;

namespace ZombieShooter.SceneObjects {
    public sealed class WeaponFactory {
        private readonly Weapon _prefab;

        public WeaponFactory(Weapon prefab) {
            _prefab = prefab;
        }

        public Weapon Get(Transform parent) {
            Weapon newWeapon = UnityEngine.Object.Instantiate(_prefab);
            newWeapon.transform.SetParent(parent);

            return newWeapon;
        }
    }
}
