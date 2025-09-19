using UpgradesSystem.Core;

namespace UpgradesSystem.UI {

    public sealed class UpgradesPopupViewModelFactory {
        private readonly IUpgradeSystem _upgradeSystem;
        private readonly UpgradePresenterFactory _upgradeFactory;

        public UpgradesPopupViewModelFactory(IUpgradeSystem upgradeSystem,
                                             UpgradePresenterFactory upgradeFactory) {

            _upgradeSystem = upgradeSystem;
            _upgradeFactory = upgradeFactory;
        }

        public UpgradesPopupViewModel Get() {
            return new UpgradesPopupViewModel(_upgradeSystem, _upgradeFactory);
        }
    }
}
