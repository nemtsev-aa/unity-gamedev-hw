using UnityEngine;
using System.Collections.Generic;
using BehaviorTree.PlayerCompanents;

namespace BehaviorTree.PlayerCoreSubsystem {

    public sealed class PlayerCore : MonoBehaviour {
        [field: SerializeField] public MoveCompanent Mover { get; private set; }
        [field: SerializeField] public CollectorCompanent Collector { get; private set; }
        [field: SerializeField] public FellerCompanent Feller { get; private set; }
        [field: SerializeField] public InventoryCompanent Inventory { get; private set; }

        private List<IPlayerCompanent> Companents = new();

        public void Init(PlayerConfig config) {
            Companents.Add(Mover);
            Companents.Add(Collector);
            Companents.Add(Feller);
            Companents.Add(Inventory);

            foreach (var iCompanent in Companents) {
                iCompanent.Init(transform, config);
            }
        }

        private void Update() {
            Feller.Update(Time.deltaTime);
        }
    }
}



