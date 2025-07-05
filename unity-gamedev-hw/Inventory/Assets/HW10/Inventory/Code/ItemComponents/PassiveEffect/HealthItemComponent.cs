using System;
using UnityEngine;

namespace InventorySystem.ItemComponents {

    [Serializable]
    public sealed class HealthItemComponent : IItemComponent {
        [SerializeField] private int _hitPoints;

        public string Type => "Health";

        public int Value => _hitPoints;

        public HealthItemComponent(int hitPoints) {
            _hitPoints = hitPoints;
        }

        public IItemComponent Clone() {
            return new HealthItemComponent(_hitPoints);
        }
    }
}

