using UnityEngine;

namespace PresentationModel {
    [CreateAssetMenu(
        fileName = nameof(CharacterPopupConfig),
        menuName = "Configs/" + nameof(CharacterPopupConfig))
    ]

    public class CharacterPopupConfig : DataConfig {
        [field: SerializeField] public UserInfoConfig UserInfoConfig { get; private set; }
        [field: SerializeField] public PlayerLevelConfig PlayerLevelConfig { get; private set; }
        [field: SerializeField] public CharacterStatConfigs StatConfigs { get; private set; }
    }
}


