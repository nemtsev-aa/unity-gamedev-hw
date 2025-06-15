using UnityEngine;
using System;

namespace UpgradesSystem.UI {

    [Serializable]
    public sealed class UpgradeDescription {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}
