using R3;
using UnityEngine;
using BehaviorTree.Brain;
using FarmingSystem;

namespace BehaviorTree.Bot {

    public interface IBotInfoViewModel {
        public ReadOnlyReactiveProperty<BotStates> CurrentBotState { get; }
        // Moving
        public ReadOnlyReactiveProperty<Transform> MoveTarget { get; }
        public ReadOnlyReactiveProperty<float> DistanceToTarget { get; }
        // Patrol
        public ReadOnlyReactiveProperty<Transform> PartolPoint { get; }
        // Farming
        public ReadOnlyReactiveProperty<WorkOperationPriority> Priority { get; }
        public ReadOnlyReactiveProperty<ResourceSpot> NearestResourceSource { get; }
        public ReadOnlyReactiveProperty<ResourceLoot> NearestResourceLoot { get; }
        public ReadOnlyReactiveProperty<int> MaxInventoryAmount { get; }
        public ReadOnlyReactiveProperty<int> CurrentInventoryAmount { get; }
    }
}



