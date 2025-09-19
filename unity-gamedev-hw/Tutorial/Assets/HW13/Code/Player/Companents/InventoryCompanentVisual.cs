using System;
using UnityEngine;
using System.Collections.Generic;

namespace BehaviorTree.PlayerCompanents {
    public sealed class InventoryCompanentVisual : MonoBehaviour, IDisposable {
        [SerializeField] private List<GameObject> _items;

        private InventoryCompanent _inventory;
        private int _currentAmount;

        public void Init(InventoryCompanent inventory) {
            _inventory = inventory;
            _inventory.CurrentAmountChanged += ShowItems;

            HideItems();
        }

        public void ShowItems(int currentAmount) {
            currentAmount = Mathf.Clamp(currentAmount, 0, _items.Count);
            _currentAmount = currentAmount;

            for (var i = 0; i < currentAmount; i++) {
                var item = _items[i];
                item.SetActive(true);
            }

            var count = _items.Count;
            for (var i = currentAmount; i < count; i++) {
                var item = _items[i];
                item.SetActive(false);
            }
        }

        private void HideItems() {

            for (var i = 0; i < _items.Count; i++) {
                var item = _items[i];
                item.SetActive(false);
            }
        }

        public void Dispose() {
            _inventory.CurrentAmountChanged -= ShowItems;
        }
    }
}

