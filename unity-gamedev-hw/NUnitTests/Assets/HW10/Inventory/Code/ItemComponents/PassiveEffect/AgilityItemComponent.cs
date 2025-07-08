using System;
using UnityEngine;

namespace InventorySystem.ItemComponents {

    [Serializable]
    public sealed class AgilityItemComponent : IPassiveEffectItemComponent {
        [SerializeField] private int _agility;

        public string Type => "Agility";

        public int Value => _agility;

        public AgilityItemComponent(int agility) {
            _agility = agility;
        }

        public IItemComponent Clone() {
            return new AgilityItemComponent(_agility);
        }
    }
}

