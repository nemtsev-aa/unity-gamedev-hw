using UnityEngine;

namespace CursorChangeService {
    public interface ICursorChangeRaycasterViewModel {
        float RaycastDistance { get; }
        LayerMask InteractableLayers { get; }
    }
}

