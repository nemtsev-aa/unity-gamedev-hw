using Client.Components.Teams;
using System;
using UnityEngine;

namespace Client.Installer {
    [Serializable]
    public sealed class TeamMaterial {
        [field: SerializeField] public TeamTypes Type { get; private set; }
        [field: SerializeField] public Material Material { get; private set; }
    }
}

