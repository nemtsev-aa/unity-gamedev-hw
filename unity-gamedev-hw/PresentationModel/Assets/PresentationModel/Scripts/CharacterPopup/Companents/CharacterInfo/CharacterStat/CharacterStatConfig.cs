using UnityEngine;
using System;

namespace PresentationModel {
    [Serializable]
    public class CharacterStatConfig {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int Value { get; private set; }
    }
}


