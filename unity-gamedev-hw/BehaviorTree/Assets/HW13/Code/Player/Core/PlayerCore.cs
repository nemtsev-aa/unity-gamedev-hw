using UnityEngine;
using BehaviorTree.PlayerCompanents;

namespace BehaviorTree.PlayerCoreSubsystem {

    public sealed class PlayerCore : MonoBehaviour {
        [field: SerializeField] public MoveCompanent Mover { get; private set; }
        [field: SerializeField] public CollectorCompanent Collector { get; private set; }
        [field: SerializeField] public InventoryCompanent Inventory { get; private set; }
        
        public void Init(PlayerConfig config) {
            Mover.Init(transform, config);
            Collector.Init(transform, config);
            Inventory.Init(transform, config);
        }
    }
}



