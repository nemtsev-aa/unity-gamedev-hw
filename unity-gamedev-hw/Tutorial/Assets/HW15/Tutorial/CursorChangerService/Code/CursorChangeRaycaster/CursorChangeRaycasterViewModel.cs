using UnityEngine;

namespace CursorChangeService {
    public sealed class CursorChangeRaycasterViewModel : ICursorChangeRaycasterViewModel {
        public float RaycastDistance => _config.RaycastDistance;
        public LayerMask InteractableLayers => _config.InteractableLayers;

        private readonly CursorChangeServiceConfig _config;

        public CursorChangeRaycasterViewModel(CursorChangeServiceConfig config) {
            _config = config;
        }
    }
}

