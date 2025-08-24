using System;
using UI.Core;
using UnityEngine;

namespace UI.Components.Screens {

    [Serializable]
    public class UIScreenConfig {
        [field: SerializeField] public UIScreenType ScreenType { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public UILayer Layer { get; private set; }
        [field: SerializeField] public bool DestroyOnHide { get; private set; }
        [field: SerializeField] public bool AllowMultipleInstances { get; private set; }

        public UIScreenConfig(UIScreenType screenType,
                              GameObject prefab,
                              UILayer layer,
                              bool destroyOnHide,
                              bool allowMultipleInstances) {

            ScreenType = screenType;
            Prefab = prefab;
            Layer = layer;
            DestroyOnHide = destroyOnHide;
            AllowMultipleInstances = allowMultipleInstances;
        }
    }
}
