using Client.Components;
using Client.Components.Common;
using System.Collections.Generic;
using UnityEngine;

namespace Client.Installer {

    [CreateAssetMenu(
        fileName = nameof(UnitBaseConfigs),
        menuName = "UnitBaseConfigs/" + nameof(UnitBaseConfigs)
    )]

    public sealed class UnitBaseConfigs : ScriptableObject {
        [field: SerializeField] public List<UnitBaseConfig> Configs { get; private set; }

        public bool TryGetConfigByType(UnitTypes type, out UnitBaseConfig config) {

            for (int i = 0; i < Configs.Count; i++) {
                
                if (Configs[i].Type == type) {
                    config = Configs[i];
                    return true;
                }
            }

            config = null;
            return false;
        }
    }
}

