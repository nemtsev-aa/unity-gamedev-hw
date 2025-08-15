using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace LevelZoneSystem {
    
    [Serializable]
    public sealed class ZoneReferenceConfig {
        [field: SerializeField] public int Index { get; private set; }
        [field: SerializeField] public AssetReference Reference { get; private set; }
    }
}