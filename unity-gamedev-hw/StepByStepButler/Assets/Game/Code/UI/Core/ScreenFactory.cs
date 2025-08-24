using UnityEngine;
using UI.Components;
using UI.Components.Screens;
using System.Collections.Generic;
using Object = UnityEngine.Object;
using static UnityEngine.Rendering.STP;
using Unity.VisualScripting.FullSerializer;

namespace UI.Services {

    public sealed class ScreenFactory {
        private readonly UIScreenConfigs _screenConfigs;
        private readonly Dictionary<UIScreenType, Queue<UIComponent>> _screenPool = new();

        public ScreenFactory(UIScreenConfigs screenConfigs) {
            _screenConfigs = screenConfigs;
        }

        public T CreateScreen<T>(UIScreenType screenType, Transform parent) where T : UIComponent {

            if (TryGetFromPool(screenType, out var screen) == true)
                return (T)screen;

            if (TryGetScreenPrefabByType(screenType, out var prefab) == false)
                return null;

            var instance = Object.Instantiate(prefab, parent);
            var component = instance.GetComponent<T>();

            if (component == null) {
                Debug.LogError($"Component {typeof(T)} not found on {screenType} prefab");
                Object.Destroy(instance);
                return null;
            }

            component.Initialize();

            return component;
        }

        private bool TryGetFromPool(UIScreenType screenType, out UIComponent screen) {

            if (_screenPool.TryGetValue(screenType, out var pool) && pool.Count > 0) {
                screen = pool.Dequeue();
                return true;
            }

            screen = null;
            return false;
        }

        private bool TryGetScreenPrefabByType(UIScreenType type, out GameObject prefab) {

            for (int i = 0; i < _screenConfigs.Configs.Length; i++) {
                var iConfig = _screenConfigs.Configs[i];

                if (iConfig.ScreenType == type) {
                    prefab = iConfig.Prefab;
                    return true;
                }
            }

            prefab = null;
            return false;
        }
    }
}
