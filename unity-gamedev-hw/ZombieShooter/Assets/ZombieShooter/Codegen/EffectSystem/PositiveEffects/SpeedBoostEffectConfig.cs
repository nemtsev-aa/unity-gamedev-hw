using UnityEngine;

namespace AtomicFramework.Effects {

    [CreateAssetMenu(
        fileName = nameof(SpeedBoostEffectConfig),
        menuName = "Configs/Effects/" + nameof(SpeedBoostEffectConfig)
    )]

    public sealed class SpeedBoostEffectConfig : EffectConfig {
        [field: SerializeField] public int ActivateCondition { get; private set; }
    }
}
