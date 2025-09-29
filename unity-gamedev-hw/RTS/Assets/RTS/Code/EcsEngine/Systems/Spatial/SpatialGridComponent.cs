using UnityEngine;
using System;

namespace Client.Components.Spatial {

    [Serializable]
    public struct SpatialGridComponent {
        public int GridKey;
        public Vector3 LastPosition;
    }
}
