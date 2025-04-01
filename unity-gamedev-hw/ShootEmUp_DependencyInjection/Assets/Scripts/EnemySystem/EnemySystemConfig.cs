using UnityEngine;

namespace ShootEmUp {
    [CreateAssetMenu(
        fileName = nameof(EnemySystemConfig),
        menuName = "Configs/" + nameof(EnemySystemConfig)
    )]

    public sealed class EnemySystemConfig : ScriptableObject {
        [field: SerializeField] public Enemy Prefab { get; private set; }
        [field: SerializeField] public int PoolSize { get; private set; } = 7;
        [field: SerializeField] public float SpawnDelay { get; private set; } = 0.5f;
    }
}
