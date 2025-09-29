using Leopotam.EcsLite.Entities;
using System;

namespace Client.Components.Projectile {
    
    [Serializable]
    public struct ProjectileData {
        public float Speed;
        public int Damage;
        public Entity OwnerEntity; 
        public Entity TargetEntity; 
    }
}