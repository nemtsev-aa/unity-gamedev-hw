namespace UpgradesSystem.ConveyorUpgrades {

    public class UnloadStorageUpgrade : ConveyorUpgrade {
        private UnloadStorageUpgradeConfig _config;

        public UnloadStorageUpgrade(UnloadStorageUpgradeConfig config) : base(config) {
            _config = config;
        }

        protected override void OnUpgrade() {
            var storage = Conveyor.Core.UnloadStorage;
            var amount = (int)_config.Table.GetValue(Level);

            storage.MaxValue = amount;
        }
    }
}