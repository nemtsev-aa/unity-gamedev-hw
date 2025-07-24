namespace UpgradesSystem.ConveyorUpgrades {

    public class LoadStorageUpgrade : ConveyorUpgrade {
        private LoadStorageUpgradeConfig _config;

        public LoadStorageUpgrade(LoadStorageUpgradeConfig config) : base(config) {
            _config = config;
        }

        protected override void OnUpgrade() {
            var storage = Conveyor.Core.LoadStorage;
            var amount = (int)_config.Table.GetValue(Level);

            storage.MaxValue = amount;
        }
    }
}