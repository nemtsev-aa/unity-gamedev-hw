using System;
using UnityEngine;
using Client.Components.Common;

namespace GameCycleSystem {

    [Serializable]
    public sealed class UnitPoolArgs {
        [field: SerializeField] public UnitTypes Type { get; private set; }
        [field: SerializeField] public int Size { get; private set; }
    }
}
