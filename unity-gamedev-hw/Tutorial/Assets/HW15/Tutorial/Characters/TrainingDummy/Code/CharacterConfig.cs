using UnityEngine;

namespace Characters {

    [CreateAssetMenu(
        fileName = nameof(CharacterConfig),
        menuName = "Tutorial/Configs/" + nameof(CharacterConfig)
    )]
    public sealed class CharacterConfig : ScriptableObject {
        [field: SerializeField] public int HitPointMaxValue { get; private set; }
    }
}