using System;
using UI.Core;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace UI.Components.Screens {

    [Serializable]
    public class UIScreenConfig {
        [field: SerializeField] public UIScreenType ScreenType { get; private set; }
        [field: SerializeField] public AssetReference PrefabPath { get; private set; }
        [field: SerializeField] public UILayer Layer { get; private set; }
        [field: SerializeField] public bool DestroyOnHide { get; private set; }
        [field: SerializeField] public bool AllowMultipleInstances { get; private set; }

        public UIScreenConfig(UIScreenType screenType,
                              AssetReference prefabPath,
                              UILayer layer,
                              bool destroyOnHide,
                              bool allowMultipleInstances) {

            ScreenType = screenType;
            PrefabPath = prefabPath;
            Layer = layer;
            DestroyOnHide = destroyOnHide;
            AllowMultipleInstances = allowMultipleInstances;
        }
    }
}
