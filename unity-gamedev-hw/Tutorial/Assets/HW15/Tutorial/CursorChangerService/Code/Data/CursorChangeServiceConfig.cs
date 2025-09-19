using System.Collections.Generic;
using UnityEngine;

namespace CursorChangeService {
    [CreateAssetMenu(
        fileName = nameof(CursorChangeServiceConfig),
        menuName = "CursorChangeService/Config/" + nameof(CursorChangeServiceConfig)
    )]
    public sealed class CursorChangeServiceConfig : ScriptableObject {
        [field: SerializeField] public CursorData DefaultCursor { get; private set; }
        [field: SerializeField] public List<CursorData> CursorDataList { get; private set; }
        [field: SerializeField] public float RaycastDistance { get; private set; } = 100f;
        [field: SerializeField] public LayerMask InteractableLayers { get; private set; } = ~0;
    }
}

