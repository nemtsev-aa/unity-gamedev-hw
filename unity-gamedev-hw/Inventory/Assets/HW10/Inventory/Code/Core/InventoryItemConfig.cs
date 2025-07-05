using InventorySystem.ItemComponents;
using System;
using UnityEngine;

namespace InventorySystem.Core {

    [CreateAssetMenu(
        fileName = nameof(InventoryItemConfig),
        menuName = "InventorySystem/New " + nameof(InventoryItemConfig)
    )]
    public sealed class InventoryItemConfig : ScriptableObject {
        public string Id => _prototype.ID;

        [SerializeField] private InventoryItem _prototype;

        public InventoryItem Clone() {
            return _prototype.Clone();
        }

        private void OnValidate() {

            bool hasStackableFlag = _prototype.Flags.HasFlag(InventoryItemFlags.STACKABLE);
            bool hasStackableCompanent = _prototype.TryGetComponent<StackableItemComponent>(out var component);

            if (hasStackableFlag == hasStackableCompanent)
                return;

            if (hasStackableFlag == false && hasStackableCompanent == true)
                throw new Exception($"{this.name} not contains a flag [STACKABLE]," +
                      $" but does contains a [StackableItemComponent]");

            if (hasStackableFlag == true && hasStackableCompanent == false)
                throw new Exception($"{this.name} contains a flag [STACKABLE]," +
                        $" but does not contain a [StackableItemComponent]");
        }
    }
}

