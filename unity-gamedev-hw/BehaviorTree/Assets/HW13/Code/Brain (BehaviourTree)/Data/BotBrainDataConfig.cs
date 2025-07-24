using System;
using UnityEngine;
using FarmingSystem;

namespace BehaviorTree.Brain {

    [Serializable]
    public sealed class BotBrainDataConfig {

        [field: SerializeField] public Transform Root { get; private set; }
        [field: SerializeField] public Transform DeliveryTarget { get; private set; }
        [field: SerializeField] public PatrolWaypoints Waypoints { get; private set; }
        [field: SerializeField] public FellingZones FellingZones { get; private set; }
        [field: SerializeField] public WorkOperationPriority Priority { get; private set; }
    }
}