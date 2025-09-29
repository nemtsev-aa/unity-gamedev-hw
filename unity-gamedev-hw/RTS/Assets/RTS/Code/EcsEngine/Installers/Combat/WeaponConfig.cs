using UnityEngine;

namespace Client.Installer {
    public class WeaponConfig : ScriptableObject {
        [field: SerializeField] public float ScaningRange { get; private set; } = 20f;
        [field: SerializeField] public float AttackRange { get; private set; } = 2f;
        [field: SerializeField] public float AttackRate { get; private set; } = 2f;
    }
}

