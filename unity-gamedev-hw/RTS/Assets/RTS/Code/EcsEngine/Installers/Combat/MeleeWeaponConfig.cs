using UnityEngine;

namespace Client.Installer {
    
    [CreateAssetMenu(
       fileName = nameof(MeleeWeaponConfig),
       menuName = "WeaponConfigs/" + nameof(MeleeWeaponConfig)
    )]
    public class MeleeWeaponConfig : WeaponConfig {
        [field: SerializeField] public int Damage { get; private set; } = 10;

    }
}

