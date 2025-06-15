using R3;
using System;
using ShopSystem.UI;

namespace UpgradesSystem.UI {
    public interface IUpgradeViewModel : IViewModel, IDisposable {
        Observable<bool> CanUpgrade { get; }
        ReactiveCommand UpgradeCommand { get; }
        void LevelUp();
    }
}
