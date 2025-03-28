using ShootEmUp;
using UnityEngine;

[CreateAssetMenu(
    fileName = nameof(CharacterConfig),
    menuName = "Configs/" + nameof(CharacterConfig)
)]

public class CharacterConfig : ScriptableObject {
    [field: SerializeField] public int HitPointCount { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public BulletConfig BulletConfig { get; private set; }
}
