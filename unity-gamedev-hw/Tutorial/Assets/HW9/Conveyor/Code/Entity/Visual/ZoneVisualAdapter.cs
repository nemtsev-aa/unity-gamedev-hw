using Elementary;
using Conveyors.Entity.Core;

namespace Conveyors.Entity.Visual {

    public sealed class ZoneVisualAdapter : IAwakeListener,
                                            IEnableListener,
                                            IDisableListener {

        private IVariableLimited<int> _storage;
        private ZoneVisual _visualZone;

        public void Construct(IVariableLimited<int> storage, ZoneVisual visualZone) {
            _storage = storage;
            _visualZone = visualZone;
        }

        void IAwakeListener.Awake() {
            _visualZone.SetupItems(_storage.Current);
        }

        void IEnableListener.OnEnable() {
            _storage.OnValueChanged += OnItemsChanged;
        }

        void IDisableListener.OnDisable() {
            _storage.OnValueChanged -= OnItemsChanged;
        }

        private void OnItemsChanged(int count) {
            _visualZone.SetupItems(count);
        }
    }
}