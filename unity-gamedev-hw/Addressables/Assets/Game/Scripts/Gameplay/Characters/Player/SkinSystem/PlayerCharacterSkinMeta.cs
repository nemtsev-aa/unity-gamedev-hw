using System;
using UnityEngine;

namespace CharactersSystem.Player.Skins {

    [Serializable]
    public sealed class PlayerCharacterSkinMeta {
        [field: SerializeField] public int Id { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
    }
}
