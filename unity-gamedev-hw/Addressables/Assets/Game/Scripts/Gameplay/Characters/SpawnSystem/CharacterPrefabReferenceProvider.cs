using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace CharactersSystem.Spawner {

    [Serializable]
    public sealed class CharacterPrefabReferenceProvider {
        [field: SerializeField] public List<CharacterReferenceConfig> References { get; private set; }

        public bool TryGetCharacterReferenceByIndex(int index, out AssetReference reference) {

            if (References == null || References.Count == 0)
                throw new ArgumentException($"ZoneReferences is empty!");

            for (int i = 0; i < References.Count; i++) {
                var config = References[i];

                if (config.Index == index) {
                    reference = config.Reference;
                    return true;
                }
            }

            Debug.LogError($"ZoneReference with the index {index} was not found!");

            reference = null;
            return false;
        }
    }
}