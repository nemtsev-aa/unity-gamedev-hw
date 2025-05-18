using UnityEngine;

namespace ZombieShooter.SceneObjects {

    [CreateAssetMenu(
        fileName = nameof(CharacterConfig),
        menuName = "Configs/" + nameof(CharacterConfig)
    )]

    public class CharacterConfig : UnitConfig {
        [field: SerializeField] public Character Prefab { get; private set; }   
    }
}
