using System;
using System.Collections.Generic;

namespace Client.Components.Spatial {

    [Serializable]
    public sealed class SpatialGridCell {
        public readonly HashSet<int> Entities = new HashSet<int>();
    }
}
