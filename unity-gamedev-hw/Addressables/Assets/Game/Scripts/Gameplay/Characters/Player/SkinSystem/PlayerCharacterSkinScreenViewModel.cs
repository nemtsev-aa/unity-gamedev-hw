using System.Collections.Generic;

namespace CharactersSystem.Player.Skins {
    public sealed class PlayerCharacterSkinScreenViewModel : IPlayerCharacterSkinScreenViewModel {
        public IReadOnlyList<PlayerCharacterSkin> Skins { get; private set; }

        public PlayerCharacterSkinScreenViewModel(PlayerCharacterSkinManager skinManager) {
            Skins = skinManager.Skins;
        }
    }
}
