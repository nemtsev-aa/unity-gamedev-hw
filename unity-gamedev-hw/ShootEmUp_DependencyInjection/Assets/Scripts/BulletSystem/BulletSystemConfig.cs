using UnityEngine;

namespace ShootEmUp {
    [CreateAssetMenu(
        fileName = nameof(BulletSystemConfig),
        menuName = "Configs/" + nameof(BulletSystemConfig)
    )]

    public sealed class BulletSystemConfig : ScriptableObject {
        [field: SerializeField] public Bullet Prefab { get; private set; }
        [field: SerializeField] public int InitialCount { get; private set; } = 30;
        [field: SerializeField] public float PositionCheckingInterval { get; private set; } = 3f;
    }
}

