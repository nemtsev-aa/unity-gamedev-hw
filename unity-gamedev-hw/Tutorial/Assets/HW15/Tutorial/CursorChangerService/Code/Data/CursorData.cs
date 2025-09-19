using System;
using UnityEngine;

namespace CursorChangeService {

    [Serializable]
    public sealed class CursorData {
        [field: SerializeField] public CursorType Type { get; private set; }
        [field: SerializeField] public Texture2D Texture { get; private set; }
        [field: SerializeField] public Vector2 Hotspot { get; private set; } = Vector2.zero;
    }
}

