using Tutorial.UI;
using UnityEngine;

namespace InteractionService {
    public sealed class CharacterUpgradeStarter : InteractionSource {
        [field: SerializeField] public TutorialCharacterUpgradePopup Popup { get; private set; }
    }
}


