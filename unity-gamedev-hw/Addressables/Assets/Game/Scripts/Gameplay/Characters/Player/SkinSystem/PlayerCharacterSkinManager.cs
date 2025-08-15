using UnityEngine;
using System.Collections.Generic;

namespace CharactersSystem.Player.Skins {

    public sealed class PlayerCharacterSkinManager : MonoBehaviour {
        [field: SerializeField] public int CurrentSkinId { get; private set; }
        public IReadOnlyList<PlayerCharacterSkin> Skins => _skins;

        private List<PlayerCharacterSkin> _skins = new();
        private PlayerCharacterSkin _currentSkin;

        public void Init() {
            CreateCharacterSkinsList();
        }

        public void SwitchSkin(int id) {
            if (TryGetSkinById(id, out var newSkin) == false)
                return;

            if (_currentSkin != null)
                _currentSkin.Activate(false);

            _currentSkin = newSkin;
            _currentSkin.Activate(true);
        }

        private void CreateCharacterSkinsList() {

            for (int i = 0; i < transform.childCount; i++) {
                var iChild = transform.GetChild(i);

                if (iChild.TryGetComponent(out PlayerCharacterSkin skin) == true) {

                    if (_skins.Contains(skin) == false)
                        _skins.Add(skin);

                    skin.Activate(false);
                }
            }
        }

        private bool TryGetSkinById(int id, out PlayerCharacterSkin skin) {

            if (_skins.Count == 0) {
                skin = null;
                return false;
            }

            for (int i = 0; i < _skins.Count; i++) {
                var iSkin = _skins[i];

                if (iSkin.Meta.Id == id) {
                    skin = iSkin;
                    return true;
                }
            }

            Debug.Log($"Skin with the Id [{id}] was not found");
            skin = null;
            return false;
        }
    }
}
