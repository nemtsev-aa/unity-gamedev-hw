using System;
using UnityEngine;

namespace InventorySystem.ItemComponents {

    [Serializable]
    public sealed class IntelligenceItemComponent : IPassiveEffectItemComponent {
        [SerializeField] private int _intelligence;

        public IntelligenceItemComponent(int intelligence) {
            _intelligence = intelligence;
        }

        public string Type => "Intelligence";

        public int Value => _intelligence;

        public IItemComponent Clone() {
            return new IntelligenceItemComponent(_intelligence);
        }
    }
}

