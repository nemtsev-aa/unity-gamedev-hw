using System;
using UnityEngine;
using InventorySystem.ItemComponents;

namespace InventorySystem.Core {

    [Serializable]
    public class InventoryItem {
        public string ID;
        public InventoryItemFlags Flags;
        public InventoryItemMetadata Metadata;

        [SerializeReference] 
        public IItemComponent[] Components;

        public bool TryGetComponent<T>(out T component) where T : IItemComponent {
            
            foreach (var itemComponent in Components) {

                if (itemComponent is T targetComponent) {
                    component = targetComponent;
                    return true;
                }
            }

            component = default;
            return false;
        }

        public InventoryItem Clone() {

            var count = Components.Length;
            var components = new IItemComponent[count];

            for (int i = 0; i < count; i++) {
                var component = Components[i];
                
                if (component is IItemComponent cloneable) 
                    component = cloneable.Clone();

                components[i] = component;
            }

            return new InventoryItem() {
                ID = ID,
                Flags = Flags,

                Metadata = new InventoryItemMetadata(
                    Metadata.Name,
                    Metadata.Description,
                    Metadata.Icon),

                Components = components
            };
        }
    }
}

