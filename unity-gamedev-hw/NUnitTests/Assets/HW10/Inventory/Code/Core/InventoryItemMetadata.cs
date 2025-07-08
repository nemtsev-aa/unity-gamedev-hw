using System;
using UnityEngine;

namespace InventorySystem.Core {

    [Serializable]
    public sealed class InventoryItemMetadata {
        public InventoryItemMetadata(string name, string description, Sprite icon) {
            Name = name;
            Description = description;
            Icon = icon;
        }

        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}

