using ProgressService;
using UpgradesSystem.Core;

namespace Tutorial.PlayerUpgrades {

    public class PlayerUpgrade : Upgrade {

        protected PlayerProgressData PlayerProgressData;

        public PlayerUpgrade(UpgradeConfig config) : base(config) { }

        public virtual void SetPlayerProgressData(PlayerProgressData data) {
            PlayerProgressData = data;
        }

        protected override void OnUpgrade() { }
    }
}