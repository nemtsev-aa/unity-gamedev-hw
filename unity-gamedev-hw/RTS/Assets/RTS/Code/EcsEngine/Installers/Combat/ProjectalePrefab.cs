using Client.Components.Teams;
using Leopotam.EcsLite.Entities;
using UnityEngine;
using System;

namespace Client.Installer {
    [Serializable]
    public sealed class ProjectalePrefab {
        [field: SerializeField] public TeamTypes Team { get; private set; }
        [field: SerializeField] public Entity Entity { get; private set; }
    }
}



