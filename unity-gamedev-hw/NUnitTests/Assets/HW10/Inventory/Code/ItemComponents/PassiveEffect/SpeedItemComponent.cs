using System;
using UnityEngine;

namespace InventorySystem.ItemComponents {
    [Serializable]
    public sealed class SpeedItemComponent : IPassiveEffectItemComponent {
        [SerializeField] private int _speed;

        public string Type => "Speed";
        public int Value => _speed;

        public SpeedItemComponent(int agility) {
            _speed = agility;
        }

        public IItemComponent Clone() {
            return new SpeedItemComponent(_speed);
        }
    }
}

