using ProgressService;

namespace Tutorial.PlayerUpgrades {

    public class PlayerHealthUpgrade : PlayerUpgrade {
        private PlayerHealthUpgradeConfig _config;

        public PlayerHealthUpgrade(PlayerHealthUpgradeConfig config) : base(config) {
            _config = config;
        }

        public override void SetPlayerProgressData(PlayerProgressData data) {
            base.SetPlayerProgressData(data);

            var level = (int)_config.Table.GetLevel(data.HealthLevel);
            SetLevel(level);
        }

        protected override void OnUpgrade() {
            PlayerProgressData.HealthLevel = (int)_config.Table.GetValue(Level);
            //Debug.Log($"PlayerHealthUpgrade: OnUpgrade");
        }
    }
}