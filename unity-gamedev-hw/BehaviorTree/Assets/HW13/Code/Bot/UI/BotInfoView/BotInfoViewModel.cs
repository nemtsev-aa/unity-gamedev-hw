using R3;
using UnityEngine;
using FarmingSystem;
using BehaviorTree.Brain;

namespace BehaviorTree.Bot {
    public sealed class BotInfoViewModel : IBotInfoViewModel {
        public ReadOnlyReactiveProperty<BotStates> CurrentBotState => _brainData.BotState;
        // Moving
        public ReadOnlyReactiveProperty<Transform> MoveTarget => _brainData.MoveTarget;
        public ReadOnlyReactiveProperty<float> DistanceToTarget => _brainData.DistanceToTarget;
        // Patrol
        public ReadOnlyReactiveProperty<Transform> PartolPoint => _brainData.PartolPoint;
        // Farming
        public ReadOnlyReactiveProperty<WorkOperationPriority> Priority => _brainData.Priority;
        public ReadOnlyReactiveProperty<FellingZone> NearestFellingZone => _brainData.NearestFarmingZone;
        public ReadOnlyReactiveProperty<ResourceSpot> NearestResourceSource => _brainData.NearestResourceSource;
        public ReadOnlyReactiveProperty<ResourceLoot> NearestResourceLoot => _brainData.NearestResourceLoot;
        public ReadOnlyReactiveProperty<int> MaxInventoryAmount => _brainData.MaxInventoryAmount;
        public ReadOnlyReactiveProperty<int> CurrentInventoryAmount => _brainData.CurrentInventoryAmount;

        private readonly BotBrainData _brainData;

        public BotInfoViewModel(BotBrainData brainData) {
            _brainData = brainData;
        }
    }
}



