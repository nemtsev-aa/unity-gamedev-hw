using UnityEngine;
using UpgradesSystem.Core;

namespace UpgradesSystem.ConveyorUpgrades {

    [CreateAssetMenu(
        fileName = nameof(UnloadStorageUpgradeConfig),
        menuName = "Configs/Upgrade/New" + nameof(UnloadStorageUpgradeConfig)
    )]
    public class UnloadStorageUpgradeConfig : UpgradeConfig {
        [field: SerializeField] public UpdateValuesTable Table { get; private set; }

        public override Upgrade Create() {
            return new UnloadStorageUpgrade(this);
        }

        protected override void OnValidate() {
            base.OnValidate();
            Table.OnValidate(MaxLevel);
        }
    }

    


}