using Leopotam.EcsLite.Entities;
using System;

namespace Client.Components.Health {

    [Serializable]
    public struct DeathRequest {
        public Entity Value;
    }
}
