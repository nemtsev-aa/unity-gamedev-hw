using Elementary;
using Conveyors.Entity.Core;

namespace Conveyors.Entity.Visual {
    public sealed class ZoneInfoAdapter : IEnableListener,
                                          IDisableListener {

        private IVariableLimited<int> _storage;
        private ZoneInfoView _infoView;

        public void Construct(IVariableLimited<int> storage, ZoneInfoView infoView) {
            _storage = storage;
            _infoView = infoView;
        }

        void IEnableListener.OnEnable() {
            _storage.OnValueChanged += OnItemsChanged;
            _storage.OnMaxValueChanged += OnItemsMaxValueChanged;
        }

        void IDisableListener.OnDisable() {
            _storage.OnValueChanged -= OnItemsChanged;
            _storage.OnMaxValueChanged -= OnItemsMaxValueChanged;
        }

        private void OnItemsChanged(int count) {
            _infoView.UpdateInfo($"{count}/{_storage.MaxValue}");
        }

        private void OnItemsMaxValueChanged(int maxValue) {
            _infoView.UpdateInfo($"{_storage.Current}/{maxValue}");
        }
    }
}