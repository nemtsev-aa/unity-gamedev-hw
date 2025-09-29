using System;
using UnityEngine;
using Client.Components.Teams;

namespace GameCycleSystem {

    [Serializable]
    public sealed class UnitSpawnPoint {
        [field: SerializeField] public TeamTypes Team { get; private set; }
        [field: SerializeField] public Transform Transform { get; private set; }
    }
}