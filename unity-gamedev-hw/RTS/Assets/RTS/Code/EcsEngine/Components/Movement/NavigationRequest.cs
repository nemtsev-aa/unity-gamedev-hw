using System;
using UnityEngine;

namespace Client.Components.Movement {
    [Serializable]
    public struct NavigationRequest {
        public Vector3 Destination;
    }
}