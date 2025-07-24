using UnityEngine;
using UpgradesSystem.Core;

namespace UpgradesSystem.ConveyorUpgrades {

    [CreateAssetMenu(
        fileName = nameof(WorkTimeUpgradeConfig),
        menuName = "Configs/Upgrade/New" + nameof(WorkTimeUpgradeConfig)
    )]
    public class WorkTimeUpgradeConfig : UpgradeConfig {

        public override Upgrade Create() {
            return new WorkTimeUpgrade(this);
        }

        protected override void OnValidate() {
            base.OnValidate();
            Table.OnValidate(MaxLevel);
        }
    }
}