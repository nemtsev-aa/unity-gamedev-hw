using System.Collections.Generic;
using UnityEngine;

namespace Character {

    [CreateAssetMenu(
       fileName = nameof(CharacterDefaultData),
       menuName = "Configs/Character/new " + nameof(CharacterDefaultData))
    ]
    public sealed class CharacterDefaultData : ScriptableObject {
        [field: SerializeField] public List<CharacterDefaultStatData> DafaultData { get; private set; }
    }
}
