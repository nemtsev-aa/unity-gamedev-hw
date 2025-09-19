using System;

namespace BehaviorTree.PlayerCompanents {

    [Serializable]
    public sealed class InventoryItem {
        public InventoryItem(string iD, int amount) {
            ID = iD;
            Amount = amount;
        }

        public string ID { get; private set; }
        public int Amount { get; set; }
    }
}

