using UnityEngine;
using Object = UnityEngine.Object;

namespace UI.Components.ActiveEffectViewSystem {

    public sealed class ActiveEffectViewFactory {
        private readonly ActiveEffectViewConfigs _configs;
        private readonly Transform _parent;

        public ActiveEffectViewFactory(ActiveEffectViewConfigs configs,
                                       Transform parent) {
            _configs = configs;
            _parent = parent;
        }

        public bool TryGet(ActiveEffectType type, out ActiveEffectView view) {

            if (_configs.TryGetConfigByType(type, out var config) == true) {
                var iView = Object.Instantiate(config.Prefab, _parent);
                iView.Init(config);

                view = iView;
                return true;
            }

            view = null;
            return false;
        }
    }
}
