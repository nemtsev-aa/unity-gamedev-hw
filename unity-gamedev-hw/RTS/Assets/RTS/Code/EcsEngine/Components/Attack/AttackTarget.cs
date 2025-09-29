using System;
using UnityEngine;

namespace Client.Components.Attack {

    [Serializable]
    public struct AttackTarget {
        public int ID;
        public float ModelRadius;
        public Vector3 LastKnownPosition;
    }
}