using UnityEngine;

namespace PresentationModel {

    [CreateAssetMenu(
            fileName = nameof(PlayerLevelConfig),
            menuName = "Configs/" + nameof(PlayerLevelConfig)
            )]

    public sealed class PlayerLevelConfig : DataConfig {
        [field: SerializeField] public int Level { get; private set; }
        [field: SerializeField] public int Experience { get; private set; }
    }
}
