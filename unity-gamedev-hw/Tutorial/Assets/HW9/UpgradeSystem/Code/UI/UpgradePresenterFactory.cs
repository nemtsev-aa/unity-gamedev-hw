using ShopSystem.Storages;
using UpgradesSystem.Core;

namespace UpgradesSystem.UI {

    public sealed class UpgradePresenterFactory {
        private readonly IUpgradeSystem _upgradeSystem;
        private readonly MoneyStorage _moneyStorage;

        public UpgradePresenterFactory(IUpgradeSystem updateSystem, MoneyStorage moneyStorage) {
            _upgradeSystem = updateSystem;
            _moneyStorage = moneyStorage;
        }

        public UpgradeViewModel CreateUpgradeViewModel(UpgradeConfig config) {
            return new UpgradeViewModel(config, _upgradeSystem, _moneyStorage);
        }
    }
}
