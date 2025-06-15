using ShopSystem.UI;
using System.Collections.Generic;

namespace UpgradesSystem.UI {
    public interface IUpgradePopupViewModel : IViewModel {
        IReadOnlyList<IUpgradeViewModel> UpgradePresenters { get; }
    }
}
