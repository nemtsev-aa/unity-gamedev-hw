using System;
using System.Collections.Generic;
using UnityEngine;

namespace AtomicFramework.Effects {

    [CreateAssetMenu(
        fileName = nameof(EffectSystemConfig),
        menuName = "Configs/" + nameof(EffectSystemConfig)
    )]

    public sealed class EffectSystemConfig : ScriptableObject {
        [field: SerializeField] public List<EffectConfig> Configs { get; private set; }

        public EffectConfig GetConfigByType(EffectType type) {

            for (int i = 0; i < Configs.Count; i++) {

                var iConfig = Configs[i];

                if (iConfig.Type == type)
                    return iConfig;
            }

            throw new ArgumentNullException($"EffectType {type} not found in list!");
        }
    }
}
