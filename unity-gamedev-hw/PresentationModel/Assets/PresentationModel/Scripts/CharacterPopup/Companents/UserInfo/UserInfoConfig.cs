using UnityEngine;

namespace PresentationModel {
    [CreateAssetMenu(
        fileName = nameof(UserInfoConfig),
        menuName = "Configs/" + nameof(UserInfoConfig))
    ]

    public class UserInfoConfig : DataConfig {
        [field: SerializeField] public string UserName { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }        
    }
}


