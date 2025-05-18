using UnityEngine;
using AtomicFramework.BulletSystem;

namespace ZombieShooter.SceneObjects {

    [CreateAssetMenu(
        fileName = nameof(WeaponConfig),
        menuName = "Configs/" + nameof(WeaponConfig)
    )]

    public sealed class WeaponConfig : ScriptableObject {
        [field: SerializeField] public Weapon Prefab { get; private set; }
        [field: SerializeField] public Transform Target { get; private set; }
        [field: SerializeField] public Bullet BulletPrefab { get; private set; }
        [field: SerializeField] public float ReloadTime { get; private set; }
        [field: SerializeField] public int MaxBulletAmount { get; private set; }
        [field: SerializeField] public float AddBulletDelay { get; private set; }
    }
}
