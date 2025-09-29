using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Components.Weapon {

    [SerializeField]
    public struct RangeWeapon {
        public Transform FirePoint;
        public Entity ProjectalePrefab;
        public float ProjectaleSpeed;
        public int ProjectaleDamage;
    }
}