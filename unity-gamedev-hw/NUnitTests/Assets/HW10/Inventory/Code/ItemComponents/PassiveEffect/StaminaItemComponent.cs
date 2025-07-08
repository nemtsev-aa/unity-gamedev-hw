using System;
using UnityEngine;

namespace InventorySystem.ItemComponents {
    [Serializable]
    public sealed class StaminaItemComponent : IPassiveEffectItemComponent {
        [SerializeField] private int _stamina;

        public string Type => "Stamina";

        public int Value => _stamina;

        public StaminaItemComponent(int agility) {
            _stamina = agility;
        }

        public IItemComponent Clone() {
            return new StaminaItemComponent(_stamina);
        }
    }
}

