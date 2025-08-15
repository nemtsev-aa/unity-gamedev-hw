using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CharactersSystem.Spawner {

    [Serializable]
    public sealed class CharacterReferenceConfig {
        [field: SerializeField] public int Index { get; private set; }
        [field: SerializeField] public AssetReference Reference { get; private set; }
    }
}