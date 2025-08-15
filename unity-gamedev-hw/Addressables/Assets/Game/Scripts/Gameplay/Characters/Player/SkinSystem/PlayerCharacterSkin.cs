using UnityEngine;

namespace CharactersSystem.Player.Skins {

    public sealed class PlayerCharacterSkin : MonoBehaviour {
        [field: SerializeField] public PlayerCharacterSkinMeta Meta { get; private set; }
        [field: SerializeField] public bool IsActive { get; private set; }

        public void Activate(bool status) {
            gameObject.SetActive(status);
        }
    }
}
