using System;
using UnityEngine;
using Conveyors.Entity;
using ShopSystem.Storages;
using System.Collections.Generic;
using UpgradesSystem.ConveyorUpgrades;

namespace UpgradesSystem.Core {

    public class UpgradeSystem {
        private readonly UpgradeCatalog _catalog;
        private readonly MoneyStorage _moneyStorage;
        private readonly ConveyorModel _conveyor;
        private readonly Dictionary<string, Upgrade> _upgrades = new();

        public UpgradeCatalog Catalog => _catalog;

        public UpgradeSystem(UpgradeCatalog catalog,
                             MoneyStorage moneyStorage,
                             ConveyorModel conveyor) {

            _catalog = catalog;
            _moneyStorage = moneyStorage;
            _conveyor = conveyor;

            CreateUpgrades();
        }

        public bool TryLevelUp(string id) {

            if (CanUpgrade(id, out Upgrade upgrade) == false) {
                Debug.LogError($"<color=red>Upgrade {id} not complited!</color>");
                return false;
            }

            _moneyStorage.SpendMoney(upgrade.NextPrice);
            upgrade.LevelUp();

            Debug.Log($"<color=green>Upgrade {id} complited!</color>");
            return true;
        }

        public int GetUpgradeLevel(string upgradeId) {
            return _upgrades.TryGetValue(upgradeId, out var upgrade) ? upgrade.Level : 0;
        }

        private void CreateUpgrades() {

            foreach (var config in _catalog.Upgrades) {
                var upgrade = config.Create();

                if (upgrade is ConveyorUpgrade conveyorUpgrade) {
                    conveyorUpgrade.SetConveyorModel(_conveyor);
                    upgrade.LevelUp();
                }

                _upgrades.Add(config.Id, upgrade);
            }
        }

        private bool CanUpgrade(string upgradeId, out Upgrade upgrade) {
            if (_upgrades.TryGetValue(upgradeId, out Upgrade currentUpgrade) == false) {
                upgrade = null;
                throw new ArgumentException($"Upgrade with id {upgradeId} not found!");
            }

            if (currentUpgrade.IsMaxLevel == false) {

                if ((int)_moneyStorage.Money.CurrentValue >= currentUpgrade.NextPrice) {
                    upgrade = currentUpgrade;
                    return true;
                }

                throw new ArgumentException($"Not enough money for upgrade {upgradeId}!");
            }

            upgrade = null;
            throw new ArgumentException($"Upgrade {upgradeId} is already at max level!");
        }
    }
}
