namespace UpgradesSystem.ConveyorUpgrades {

    public class WorkTimeUpgrade : ConveyorUpgrade {
        private WorkTimeUpgradeConfig _config;

        public WorkTimeUpgrade(WorkTimeUpgradeConfig config) : base(config) {
            _config = config;
        }

        protected override void OnUpgrade() {
            var timer = Conveyor.Core.WorkTimer;
            var duration = _config.Table.GetValue(Level);

            timer.Duration = duration;
        }
    }
}