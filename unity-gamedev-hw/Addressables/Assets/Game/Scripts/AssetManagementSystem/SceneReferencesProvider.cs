using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AssetManagementSystem {

    [Serializable]
    public sealed class SceneReferencesProvider {
        [field: SerializeField] public AssetReference MenuSceneRef { get; private set; }
        [field: SerializeField] public AssetReference GameSceneRef { get; private set; }
    }
}
