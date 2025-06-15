using UnityEngine;

namespace UpgradesSystem.Core {

    public abstract class Upgrade {
        public string Id => _config.Id;
        public int Level => _level;
        public int MaxLevel => _config.MaxLevel;
        public int NextPrice => _config.GetNextPrice(Level + 1);
        public bool IsMaxLevel => Level == MaxLevel;

        private readonly UpgradeConfig _config;
        private int _level;

        protected Upgrade(UpgradeConfig config) {
            _config = config;
            _level = 0;
        }

        public void LevelUp() {
            if (IsMaxLevel == true) {
                Debug.Log("Is max level!");
                return;
            }

            _level++;

            OnUpgrade();
        }

        protected abstract void OnUpgrade();
    }
}
