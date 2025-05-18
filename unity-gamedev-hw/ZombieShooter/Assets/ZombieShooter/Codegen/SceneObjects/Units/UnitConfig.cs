using UnityEngine;
using AtomicFramework.LifeCompanent;
using AtomicFramework.MoveCompanent;
using AtomicFramework.RotationCompanent;

namespace ZombieShooter.SceneObjects {

    public class UnitConfig : ScriptableObject {
        [field: SerializeField] public LifeConfig Life { get; private set; }
        [field: SerializeField] public MoveConfig Move { get; private set; }
        [field: SerializeField] public RotateConfig Rotate { get; private set; }
    }
}
