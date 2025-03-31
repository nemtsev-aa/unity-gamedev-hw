using ShootEmUp;
using UnityEngine;

[CreateAssetMenu(
    fileName = nameof(EnemyConfig),
    menuName = "Configs/" + nameof(EnemyConfig)
)]

public class EnemyConfig : ScriptableObject {
    [Range(1,5)]
    [SerializeField] private int _hitPointCount;
    [Range(1, 5)]
    [SerializeField] private int _speed;

    [field: SerializeField] public float ReachedOffset { get; private set; }
    [field: SerializeField] public float FireCountdown { get; private set; }
    [field: SerializeField] public BulletConfig BulletConfig { get; private set; }

    public int HitPointCount => _hitPointCount;
    public float Speed => _speed;
}
