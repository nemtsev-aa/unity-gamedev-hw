using Sirenix.OdinInspector;
using UnityEngine;

namespace UpgradesSystem.Core {

    public class UpgradeDebug : MonoBehaviour {
        public UpgradeConfig UpgradeConfig;
        private Upgrade _upgrade;

        public void Awake() {
            _upgrade = UpgradeConfig.Create();
        }

        [Button]
        public void LevelUp() =>
            _upgrade.LevelUp();
    }
}