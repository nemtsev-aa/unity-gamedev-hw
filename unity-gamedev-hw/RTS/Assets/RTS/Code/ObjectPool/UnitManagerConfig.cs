using System;
using UnityEngine;
using Client.Components.Common;
using System.Collections.Generic;

namespace GameCycleSystem {
    [Serializable]
    public sealed class UnitManagerConfig {
        [field: SerializeField] public List<UnitPoolArgs> SpawnArgs { get; private set; }

        public bool GetArgsByType(UnitTypes type, out UnitPoolArgs args) {

            for (int i = 0; i < SpawnArgs.Count; i++) {

                if (SpawnArgs[i].Type == type) {
                    args = SpawnArgs[i];

                    return true;
                }
            }

            args = null;
            return false;
        }
    }
}
