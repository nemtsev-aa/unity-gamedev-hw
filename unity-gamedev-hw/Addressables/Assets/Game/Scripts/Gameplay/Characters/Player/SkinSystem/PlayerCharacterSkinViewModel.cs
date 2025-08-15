using UnityEngine;

namespace CharactersSystem.Player.Skins {
    public sealed class PlayerCharacterSkinViewModel : IPlayerCharacterSkinViewModel {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public Sprite Icon { get; private set; }

        public PlayerCharacterSkinViewModel(int id, string name, Sprite icon) {
            Id = id;
            Name = name;
            Icon = icon;
        }
    }
}
