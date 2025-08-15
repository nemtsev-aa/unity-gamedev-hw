using R3;
using System.Collections.Generic;

namespace CharactersSystem.Player.Skins {

    public interface IPlayerCharacterSkinScreenViewModel {
        IReadOnlyList<PlayerCharacterSkin> Skins { get; }
    }
}
