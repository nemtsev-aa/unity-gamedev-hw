using UnityEngine;
using StepByStepButler.Gameplay.Heroes;

namespace StepByStepButler.Gameplay.Systems.Audio {

    [CreateAssetMenu(
        fileName = nameof(HeroAudioConfiguration),
        menuName = "BattleSystem/" + nameof(HeroAudioConfiguration)
    )]
    public sealed class HeroAudioConfiguration : ScriptableObject {
        [SerializeField] private HeroAudioClips[] _heroAudioClips;

        public HeroAudioClips GetClips(HeroType heroType) {
            foreach (var clips in _heroAudioClips) {
                if (clips.heroType == heroType) {
                    return clips;
                }
            }
            Debug.LogWarning($"No audio clips found for hero type: {heroType}");
            return null;
        }
    }
}