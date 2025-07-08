using UnityEngine;
using System.Collections.Generic;

namespace InventorySystem.Core {

    [CreateAssetMenu(
       fileName = nameof(InventoryItemConfigProvider),
       menuName = "InventorySystem/New " + nameof(InventoryItemConfigProvider)
    )]
    public sealed class InventoryItemConfigProvider : ScriptableObject {
        private List<InventoryItemConfig> _allConfigs;

        [SerializeField] private List<InventoryItemConfig> _equipmentListConfigs;
        [SerializeField] private List<InventoryItemConfig> _weaponListConfigs;
        [SerializeField] private List<InventoryItemConfig> _consumeListConfigs;
        [SerializeField] private List<InventoryItemConfig> _craftListConfigs;

        public void Init() {

            _allConfigs = new List<InventoryItemConfig>();

            var commonList = new List<List<InventoryItemConfig>> {
                _equipmentListConfigs,
                _weaponListConfigs,
                _consumeListConfigs,
                _craftListConfigs
            };

            foreach (var iList in commonList) {

                if (iList.Count > 0) {

                    for (int i = 0; i < iList.Count; i++) {

                        var iItem = iList[i];

                        if (_allConfigs.Contains(iItem) == false)
                            _allConfigs.Add(iItem);
                        else
                            Debug.LogError($"Item [{iItem.Id}] dublicate!");
                    }
                }
            }
        }

        public int GetConfigsListCount() {
            return _allConfigs.Count;
        }

        public bool TryGetItemById(string id, out InventoryItem inventoryItem) {

            for (int i = 0; i < _allConfigs.Count; i++) {
                InventoryItemConfig config = _allConfigs[i];

                if (config.Id == id) {
                    inventoryItem = config.Clone();
                    return true;
                }
            }

            inventoryItem = default;
            return false;
        }

        public bool TryGetItemByIndex(int index, out InventoryItem inventoryItem) {

            if (index <= _allConfigs.Count - 1) {
                inventoryItem = _allConfigs[index].Clone();
                return true;
            }

            inventoryItem = default;
            return false;
        }
    }
}

