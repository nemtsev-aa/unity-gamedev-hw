using UnityEngine;
using AtomicFramework.AttackCompanent;
using AtomicFramework.CollisionMechanics;
using AtomicFramework.LifeCompanent;
using AtomicFramework.MoveCompanent;

namespace AtomicFramework.BulletSystem {

    [CreateAssetMenu(
        fileName = nameof(BulletConfig),
        menuName = "Configs/" + nameof(BulletConfig)
    )]

    public sealed class BulletConfig : ScriptableObject {
        [SerializeField] private LifeConfig _life;
        [SerializeField] private MoveConfig _move;
        [SerializeField] private AttackConfig _attack;
        [SerializeField] private CollisionConfig _collision;

        public LifeConfig Life => _life;
        public MoveConfig Move => _move;
        public AttackConfig Attack => _attack;
        public CollisionConfig Collision => _collision;
    }
}
