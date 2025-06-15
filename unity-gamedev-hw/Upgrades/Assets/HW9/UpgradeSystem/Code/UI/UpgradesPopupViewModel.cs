using UpgradesSystem.Core;
using System.Collections.Generic;

namespace UpgradesSystem.UI {

    public sealed class UpgradesPopupViewModel {
        public IReadOnlyList<UpgradeViewModel> UpgradePresenters => _upgradePresenters;

        private readonly List<UpgradeViewModel> _upgradePresenters = new();

        public UpgradesPopupViewModel(UpgradeSystem upgradeSystem, UpgradePresenterFactory factory) {

            var configsList = upgradeSystem.Catalog.Upgrades;

            for (var index = 0; index < configsList.Count; index++) {
                var config = configsList[index];
                var viewModel = factory.CreateUpgradeViewModel(config);

                _upgradePresenters.Add(viewModel);
            }
        }
    }
}
