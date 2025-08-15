using UnityEngine;
using System;

namespace CharactersSystem.Player {

    [Serializable]
    public sealed class PlayerCharacterConfig {
        [field: SerializeField] public int DefaultSkinIndex { get; private set; } = 0;
        [field: SerializeField] public float MoveSpeed { get; private set; } = 2.5f;
    }
}