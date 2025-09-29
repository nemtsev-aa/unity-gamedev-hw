using System;
using UnityEngine;
using Client.Components.Teams;
using Client.Components.Common;

namespace Units.View {

    [Serializable]
    public sealed class UnitViewConfig {
        [field: SerializeField] public UnitTypes Type { get; private set; }
        [field: SerializeField] public TeamTypes Team { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }
}