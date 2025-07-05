using System;
using UnityEngine;

namespace InventorySystem.ItemComponents {

    [Serializable]
    public sealed class DamageItemComponent : IPassiveEffectItemComponent {
        [SerializeField] private int _damage;

        public string Type => "Damage";

        public int Value => _damage;

        public DamageItemComponent(int damage) {
            _damage = damage;
        }

        public IItemComponent Clone() {
            return new DamageItemComponent(_damage);
        }
    }
}

