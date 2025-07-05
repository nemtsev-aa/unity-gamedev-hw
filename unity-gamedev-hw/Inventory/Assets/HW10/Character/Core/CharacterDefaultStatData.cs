using System;
using UnityEngine;

namespace Character {

    [Serializable]
    public class CharacterDefaultStatData {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int Value { get; private set; }
    }
}
