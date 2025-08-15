using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace LevelZoneSystem {

    [CreateAssetMenu(
        fileName = nameof(ZoneReferenceProvider),
        menuName = "Gameplay/" + nameof(ZoneReferenceProvider)
    )]
    public sealed class ZoneReferenceProvider : ScriptableObject {
        [field: SerializeField] public List<ZoneReferenceConfig> Configs { get; private set; }

        public bool TryGetZoneReferenceByIndex(int index, out AssetReference reference) {

            if (Configs == null || Configs.Count == 0)
                throw new ArgumentException($"ZoneReferences is empty!");

            for (int i = 0; i < Configs.Count; i++) {
                var config = Configs[i];

                if (config.Index == index) {
                    reference = config.Reference;
                    return true;
                }
            }

            Debug.LogError($"ZoneReference with the index {index} was not found!");

            reference = null;
            return false;
        }

        public bool TryGetConfig(int index, out ZoneReferenceConfig config) {
            
            if (Configs == null || Configs.Count == 0)
                throw new ArgumentException($"ZoneReferences is empty!");

            for (int i = 0; i < Configs.Count; i++) {
                var iConfig = Configs[i];

                if (iConfig.Index == index) {
                    config = iConfig;
                    return true;
                }
            }

            Debug.LogError($"ZoneReferenceConfig with the index {index} was not found!");

            config = null;
            return false;
        }
    }
}