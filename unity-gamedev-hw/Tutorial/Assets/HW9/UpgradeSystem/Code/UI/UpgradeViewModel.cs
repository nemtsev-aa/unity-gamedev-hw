using R3;
using UnityEngine;
using ShopSystem.Storages;
using UpgradesSystem.Core;

namespace UpgradesSystem.UI {

    public sealed class UpgradeViewModel : IUpgradeViewModel {
        public string Name { get; private set; }
        public Sprite Icon { get; private set; }
        public int CurrentLevel => GetCurrentLevel();
        public string Stats => GetStatus();
        public string LevelInfo => $"{CurrentLevel} / {_config.MaxLevel}";
        public Observable<bool> CanUpgrade => _canUpgrade;
        public ReactiveCommand UpgradeCommand { get; private set; }
        public Observable<bool> OnLevelUp => _onLevelUp;

        private readonly UpgradeConfig _config;
        private readonly IUpgradeSystem _upgradeSystem;

        private readonly ReactiveProperty<bool> _canUpgrade = new();
        private readonly Subject<bool> _onLevelUp = new();
        private readonly CompositeDisposable _compositeDisposable = new();

        public UpgradeViewModel(UpgradeConfig config,
                                IUpgradeSystem upgradeSystem,
                                MoneyStorage moneyStorage) {

            _config = config;
            _upgradeSystem = upgradeSystem;

            UpdateCompanents();
            CreteReactiveSubscribes(moneyStorage);
        }

        ~UpgradeViewModel() {
            if (_compositeDisposable.IsDisposed == false)
                _compositeDisposable.Dispose();
        }

        public void LevelUp() {

            if (_upgradeSystem.TryLevelUp(_config.Id) == true)
                _onLevelUp.OnNext(true);
        }

        private void UpdateCompanents() {
            Name = _config.Description.Name;
            Icon = _config.Description.Icon;
        }

        private void CreteReactiveSubscribes(MoneyStorage moneyStorage) {
            moneyStorage.Money
                .Subscribe(OnMoneyChange)
                .AddTo(_compositeDisposable);

            UpgradeCommand = new ReactiveCommand(CanUpgrade, false);
            UpgradeCommand.Subscribe(OnBuyCommand).AddTo(_compositeDisposable);
        }

        private void OnBuyCommand(Unit _) => LevelUp();

        private int GetCurrentLevel() {
            return _upgradeSystem.GetUpgradeLevel(_config.Id);
        }

        private string GetStatus() {
            float currentLevelValue = _config.Table.GetValue(CurrentLevel);

            if (CurrentLevel < _config.MaxLevel) {
                float nextLevelValue = 0;
                float offset = 0;
                string offsetChar = "";

                if (CurrentLevel + 1 <= _config.MaxLevel + 1) {
                    nextLevelValue = _config.Table.GetValue(CurrentLevel + 1);
                    offset = nextLevelValue - currentLevelValue;

                    if (offset > 0)
                        offsetChar = "+";

                    return $"{currentLevelValue} ({offsetChar}{offset.ToString("0.##")})";
                }

                return $"{currentLevelValue}";
            }

            return $"{currentLevelValue}";
        }

        private void OnMoneyChange(long money) {

            if (TryGetNextPrice(out int nextPrice) == false) {
                _canUpgrade.Value = false;
                return;
            }

            if (money < nextPrice) {
                _canUpgrade.Value = false;
                return;
            }

            _canUpgrade.Value = true;
        }

        public bool TryGetNextPrice(out int nextPrice) {

            if (CurrentLevel + 1 < _config.MaxLevel + 1) {
                nextPrice = _config.GetNextPrice(CurrentLevel + 1);
                return true;
            }

            nextPrice = 0;
            return false;
        }

        public void Dispose() {
            if (_compositeDisposable.IsDisposed == false)
                _compositeDisposable.Dispose();
        }
    }
}
