using UnityEngine;
using AtomicFramework.AttackCompanent;

namespace ZombieShooter.SceneObjects {

    [CreateAssetMenu(
        fileName = nameof(EnemyConfig),
        menuName = "Configs/" + nameof(EnemyConfig)
    )]

    public class EnemyConfig : UnitConfig {
        [field: SerializeField] public AttackConfig Attack { get; private set; }
    }
}