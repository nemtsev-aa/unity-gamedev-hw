using UnityEngine;

namespace AtomicFramework.BulletSystem {

    [CreateAssetMenu(
        fileName = nameof(BulletSystemConfig),
        menuName = "Configs/" + nameof(BulletSystemConfig)
    )]

    public sealed class BulletSystemConfig : ScriptableObject {
        [field: SerializeField] public Bullet Prefab { get; private set; }
        [field: SerializeField] public int InitialPoolCount { get; private set; }
        [field: SerializeField] public float SpawnPeriod { get; private set; }
    }
}
