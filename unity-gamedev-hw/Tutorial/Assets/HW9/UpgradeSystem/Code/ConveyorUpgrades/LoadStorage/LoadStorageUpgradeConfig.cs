using UnityEngine;
using UpgradesSystem.Core;

namespace UpgradesSystem.ConveyorUpgrades {

    [CreateAssetMenu(
        fileName = nameof(LoadStorageUpgradeConfig),
        menuName = "Configs/Upgrade/New" + nameof(LoadStorageUpgradeConfig)
    )]
    public class LoadStorageUpgradeConfig : UpgradeConfig {

        public override Upgrade Create() {
            return new LoadStorageUpgrade(this);
        }

        protected override void OnValidate() {
            base.OnValidate();
            Table.OnValidate(MaxLevel);
        }
    }
}