using System;
using UnityEngine;

namespace EquipmentSystem.Core {
    [Serializable]
    public sealed class EquipmentSlotPosition {
        [field: SerializeField] public int Index { get; private set; }
        [field: SerializeField] public Transform SpawnPoint { get; private set; }
    }
}
