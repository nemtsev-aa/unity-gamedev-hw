using UnityEngine;

namespace ShootEmUp {
    [CreateAssetMenu(
        fileName = nameof(BulletConfig),
        menuName = "Configs/" + nameof(BulletConfig)
    )]

    public sealed class BulletConfig : ScriptableObject {
        [Range(1, 5)] [SerializeField] private int _damage;
        [Range(0.1f, 5f)] [SerializeField] private float _speed;

        [field: SerializeField] public PhysicsLayer PhysicsLayer { get; private set; }
        [field: SerializeField] public Color Color { get; private set; }

        public int Damage => _damage;
        public float Speed => _speed;
    }
}