using UnityEngine;
using UpgradesSystem.Core;

namespace Tutorial.PlayerUpgrades {

    [CreateAssetMenu(
        fileName = nameof(PlayerSpeedUpgradeConfig),
        menuName = "Configs/Upgrade/New" + nameof(PlayerSpeedUpgradeConfig)
    )]
    public class PlayerSpeedUpgradeConfig : UpgradeConfig {

        public override Upgrade Create() {
            return new PlayerSpeedUpgrade(this);
        }

        protected override void OnValidate() {
            base.OnValidate();
            Table.OnValidate(MaxLevel);
        }
    }
}