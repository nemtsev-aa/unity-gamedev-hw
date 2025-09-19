using ProgressService;

namespace Tutorial.PlayerUpgrades {

    public class PlayerSpeedUpgrade : PlayerUpgrade {
        private PlayerSpeedUpgradeConfig _config;

        public PlayerSpeedUpgrade(PlayerSpeedUpgradeConfig config) : base(config) {
            _config = config;
        }

        public override void SetPlayerProgressData(PlayerProgressData data) {
            base.SetPlayerProgressData(data);

            var level = (int)_config.Table.GetLevel(data.SpeedLevel);
            SetLevel(level);
        }

        protected override void OnUpgrade() {
            PlayerProgressData.SpeedLevel = (int)_config.Table.GetValue(Level);
        }
    }
}