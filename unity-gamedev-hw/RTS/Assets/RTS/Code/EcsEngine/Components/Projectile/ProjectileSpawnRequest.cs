using Leopotam.EcsLite.Entities;
using System;

namespace Client.Components.Projectile {

    [Serializable]
    public struct ProjectileSpawnRequest {
        public Entity OwnerEntity;
    }
}