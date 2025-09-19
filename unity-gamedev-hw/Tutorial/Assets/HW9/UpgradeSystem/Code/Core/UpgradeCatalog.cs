using System;
using UnityEngine;
using System.Collections.Generic;

namespace UpgradesSystem.Core {

    [Serializable]
    public sealed class UpgradeCatalog {
        [field: SerializeField] public List<UpgradeConfig> Upgrades { get; private set; }

        public UpgradeConfig GetUpgradeById(string id) {

            for (int i = 0; i < Upgrades.Count; i++) {
                var config = Upgrades[i];

                if (config.Id == id)
                    return config;
            }

            throw new ArgumentNullException($"Upgrade [{id} not found!]");
        }
    }
}
