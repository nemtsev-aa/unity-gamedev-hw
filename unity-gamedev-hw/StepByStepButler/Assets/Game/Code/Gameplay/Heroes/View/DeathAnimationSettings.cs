using System.Collections.Generic;
using UnityEngine;

namespace StepByStepButler.Gameplay.Heroes {

    [CreateAssetMenu(
        fileName = nameof(DeathAnimationSettings),
        menuName = "BattleSystem/" + nameof(DeathAnimationSettings)
    )]
    public partial class DeathAnimationSettings : ScriptableObject {
        [Header("Animation Settings")]
        [field: SerializeField] public float ShakeDuration { get; private set; } = 0.5f;
        [field: SerializeField] public float ShakeStrength { get; private set; } = 10f;
        [field: SerializeField] public int ShakeVibrato { get; private set; } = 10;
        [field: SerializeField] public float FadeDuration { get; private set; } = 0.7f;
        [field: SerializeField] public float ScaleDownMultiplier { get; private set; } = 0.8f;

        [Header("Effects Settings")]
        [field: SerializeField] public List<EffectPair> Effects { get; private set; } = new();
        [field: SerializeField] public Sprite DeathIcon { get; private set; }

        public Dictionary<HeroType, GameObject> GetEffectsDictionary() {
            var dict = new Dictionary<HeroType, GameObject>();

            foreach (var effect in Effects) {
                dict[effect.HeroType] = effect.EffectPrefab;
            }

            return dict;
        }

        public bool TryGetEffect(HeroType heroType, out GameObject effectPrefab) {

            for (int i = 0; i < Effects.Count; i++) {
                EffectPair iEffectPair = Effects[i];

                if (iEffectPair.HeroType == heroType) {
                    effectPrefab = iEffectPair.EffectPrefab;
                    return true;
                }
            }

            effectPrefab = null;
            return false;
        }
    }
}