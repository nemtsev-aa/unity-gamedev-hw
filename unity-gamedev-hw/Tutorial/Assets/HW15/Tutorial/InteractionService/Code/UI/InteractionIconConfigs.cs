using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace InteractionService {

    [CreateAssetMenu(
        fileName = nameof(InteractionIconConfigs),
        menuName = "Configs/" + nameof(InteractionIconConfigs)
    )]
    public class InteractionIconConfigs : ScriptableObject {
        [SerializeField] private List<InteractionIcon> _icons;

        public Sprite GetSpriteByInteractionType(InteractionTypes interactionType) {
            return _icons.FirstOrDefault(i => i.Type == interactionType).Icon;
        }
    }
}