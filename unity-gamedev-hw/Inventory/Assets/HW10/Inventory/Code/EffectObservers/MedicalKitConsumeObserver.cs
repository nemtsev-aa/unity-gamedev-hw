using UnityEngine;
using InventorySystem.Core;
using InventorySystem.ItemComponents;


namespace InventorySystem.EffectObservers {
    public sealed class MedicalKitConsumeObserver {
        private readonly InventoryItemConsumer _consumer;

        public MedicalKitConsumeObserver(InventoryItemConsumer consumer) {
            _consumer = consumer;
            _consumer.ItemConsumed += OnItemConsumed;
        }

        private void OnItemConsumed(InventoryItem item) {

            if (item.TryGetComponent<HealthItemComponent>(out var component) == true) 
                Debug.Log($"Add {component.Type} Effect: +{component.Value}");
        }
    }
}

