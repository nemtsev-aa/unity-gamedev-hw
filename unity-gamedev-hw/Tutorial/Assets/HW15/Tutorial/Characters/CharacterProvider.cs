using System;
using UnityEngine;
using System.Collections.Generic;

namespace Characters {
    [Serializable]
    public sealed class CharacterProvider {
        [field: SerializeField] public List<Character> Characters { get; private set; }

        public bool TryGetCharacterByType(CharacterTypes type, out Character character) {

            for (int i = 0; i < Characters.Count; i++) {
                var iCharacter = Characters[i];

                if (iCharacter.Type == type) {
                    character = iCharacter;
                    return true;
                }
            }

            character = null;
            return false;
        }
    }
}