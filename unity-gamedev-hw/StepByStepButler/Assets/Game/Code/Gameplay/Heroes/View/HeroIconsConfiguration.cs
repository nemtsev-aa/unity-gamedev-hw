using UnityEngine;

namespace StepByStepButler.Gameplay.Heroes {

    [CreateAssetMenu(
        fileName = nameof(HeroIconsConfiguration),
        menuName = "BattleSystem/" + nameof(HeroIconsConfiguration)
    )]
    public sealed class HeroIconsConfiguration : ScriptableObject {
 
        [SerializeField] private HeroIconPair[] _heroIcons;

        public Sprite GetIcon(HeroType heroType) {
            
            foreach (var pair in _heroIcons) {
                
                if (pair.heroType == heroType) {
                    return pair.icon;
                }
            }

            Debug.LogWarning($"No icon found for hero type: {heroType}");
            return null;
        }
    }
}