using Conveyors.Entity;
using UpgradesSystem.Core;

namespace UpgradesSystem.ConveyorUpgrades {
    public class ConveyorUpgrade : Upgrade {

        protected ConveyorModel Conveyor;

        public ConveyorUpgrade(UpgradeConfig config) : base(config) { }

        public void SetConveyorModel(ConveyorModel conveyor) {
            Conveyor = conveyor;
        }

        protected override void OnUpgrade() { }
    }
}