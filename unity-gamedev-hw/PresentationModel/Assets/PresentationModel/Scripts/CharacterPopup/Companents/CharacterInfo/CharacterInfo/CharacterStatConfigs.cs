using UnityEngine;
using System.Collections.Generic;

namespace PresentationModel {
    [CreateAssetMenu(
       fileName = nameof(CharacterStatConfigs),
       menuName = "Configs/" + nameof(CharacterStatConfigs))
    ]

    public class CharacterStatConfigs : DataConfig {
        [field: SerializeField] public List<CharacterStatConfig> Configs { get; private set; }
    }
}


