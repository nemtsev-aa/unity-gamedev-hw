using System;
using UnityEngine.AI;

namespace Client.Components.Movement {

    [Serializable]
    public struct NavMeshAgentComponent {
        public NavMeshAgent Agent;
        public float StoppingDistance;
    }
}