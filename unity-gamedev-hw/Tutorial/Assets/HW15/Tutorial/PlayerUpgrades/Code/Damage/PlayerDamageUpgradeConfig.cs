using UnityEngine;
using UpgradesSystem.Core;

namespace Tutorial.PlayerUpgrades {

    [CreateAssetMenu(
        fileName = nameof(PlayerDamageUpgradeConfig),
        menuName = "Configs/Upgrade/New" + nameof(PlayerDamageUpgradeConfig)
    )]
    public class PlayerDamageUpgradeConfig : UpgradeConfig {

        public override Upgrade Create() {
            return new PlayerDamageUpgrade(this);
        }

        protected override void OnValidate() {
            base.OnValidate();
            Table.OnValidate(MaxLevel);
        }
    }
}