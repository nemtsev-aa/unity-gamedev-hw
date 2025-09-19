using UpgradesSystem.Core;
using System.Collections.Generic;
using ShopSystem.UI;

namespace UpgradesSystem.UI {

    public sealed class UpgradesPopupViewModel {
        public IReadOnlyList<UpgradeViewModel> UpgradePresenters => _upgradePresenters;

        private readonly List<UpgradeViewModel> _upgradePresenters = new();

        public UpgradesPopupViewModel(IUpgradeSystem upgradeSystem, UpgradePresenterFactory factory) {

            var configsList = upgradeSystem.Catalog.Upgrades;

            for (var index = 0; index < configsList.Count; index++) {
                var config = configsList[index];
                var viewModel = factory.CreateUpgradeViewModel(config);

                _upgradePresenters.Add(viewModel);
            }
        }

        public bool TryGetUpgradeViewModelByName(string name, out UpgradeViewModel viewModel) {

            for (int i = 0; i < _upgradePresenters.Count; i++) {
                var iViewModel = _upgradePresenters[i];

                if (iViewModel.Name == name) {
                    viewModel = iViewModel;
                    return true;
                }
            }

            viewModel = null;
            return false;
        }
    }
}
