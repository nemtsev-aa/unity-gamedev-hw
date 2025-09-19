using UnityEngine;
using UpgradesSystem.Core;

namespace Tutorial.PlayerUpgrades {

    [CreateAssetMenu(
        fileName = nameof(PlayerHealthUpgradeConfig),
        menuName = "Configs/Upgrade/New" + nameof(PlayerHealthUpgradeConfig)
    )]
    public class PlayerHealthUpgradeConfig : UpgradeConfig {

        public override Upgrade Create() {
            return new PlayerHealthUpgrade(this);
        }

        protected override void OnValidate() {
            base.OnValidate();
            Table.OnValidate(MaxLevel);
        }
    }
}