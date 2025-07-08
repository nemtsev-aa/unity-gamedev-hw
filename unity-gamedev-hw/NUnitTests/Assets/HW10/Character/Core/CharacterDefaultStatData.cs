using System;
using UnityEngine;

namespace Character {

    [Serializable]
    public class CharacterDefaultStatData {
        public CharacterDefaultStatData(string name, int value) {
            Name = name;
            Value = value;
        }

        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int Value { get; private set; }
    }
}
