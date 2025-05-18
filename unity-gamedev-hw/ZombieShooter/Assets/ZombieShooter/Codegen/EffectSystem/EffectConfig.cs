using UnityEngine;
using ZombieShooter.UI;

namespace AtomicFramework.Effects {

    [CreateAssetMenu(
        fileName = nameof(EffectConfig),
        menuName = "Configs/Effects/" + nameof(EffectConfig)
    )]

    public class EffectConfig : ScriptableObject {
        [field: SerializeField] public EffectType Type { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public EffectView Prefab { get; private set; }
    }
}
