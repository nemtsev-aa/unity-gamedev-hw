using UnityEngine;
using UpgradesSystem.ConveyorUpgrades;
using UpgradesSystem.UI;

namespace UpgradesSystem.Core {

    public abstract class UpgradeConfig : ScriptableObject {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public int MaxLevel { get; private set; }
        [field: SerializeField] public UpgradeDescription Description { get; private set; }
        [field: SerializeField] public UpgradePriceTable PriceTable { get; private set; }
        [field: SerializeField] public UpdateValuesTable Table { get; private set; }
        
        public abstract Upgrade Create();

        public int GetNextPrice(int level) {
            return PriceTable.GetPrice(level);
        }


        protected virtual void OnValidate() {
            PriceTable.OnValidate(MaxLevel);
        }
    }
}