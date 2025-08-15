using UnityEngine;

namespace CharactersSystem.Player.Skins {
    public interface IPlayerCharacterSkinViewModel {
        public int Id { get; }
        public string Name { get; }
        public Sprite Icon { get; }
    }
}
