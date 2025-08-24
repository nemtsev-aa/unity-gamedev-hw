using System.Collections.Generic;
using UnityEngine;

namespace UI.Components.ActiveEffectViewSystem {

    [CreateAssetMenu(
        fileName = nameof(ActiveEffectViewConfigs),
        menuName = "Configs/" + nameof(ActiveEffectViewConfigs)
    )]
    public sealed class ActiveEffectViewConfigs : ScriptableObject {
        [field: SerializeField] public List<ActiveEffectViewConfig> Configs { get; private set; }

        public bool TryGetConfigByType(ActiveEffectType type, out ActiveEffectViewConfig config) {

            for (int i = 0; i < Configs.Count; i++) {

                var iConfig = Configs[i];

                if (iConfig.Type == type) {
                    config = iConfig;
                    return true;
                }
            }

            config = null;
            return false;
        }
    }
}
