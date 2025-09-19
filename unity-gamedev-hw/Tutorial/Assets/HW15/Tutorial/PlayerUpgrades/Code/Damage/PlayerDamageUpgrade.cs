using ProgressService;

namespace Tutorial.PlayerUpgrades {

    public class PlayerDamageUpgrade : PlayerUpgrade {
        private PlayerDamageUpgradeConfig _config;

        public PlayerDamageUpgrade(PlayerDamageUpgradeConfig config) : base(config) {
            _config = config;
        }

        public override void SetPlayerProgressData(PlayerProgressData data) {
            base.SetPlayerProgressData(data);

            var level = (int)_config.Table.GetLevel(data.DamageLevel);
            SetLevel(level);
        }

        protected override void OnUpgrade() {
            PlayerProgressData.DamageLevel = (int)_config.Table.GetValue(Level);
        }
    }
}