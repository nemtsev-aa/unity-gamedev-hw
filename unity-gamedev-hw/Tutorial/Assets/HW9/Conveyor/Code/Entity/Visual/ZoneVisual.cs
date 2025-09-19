using Sirenix.OdinInspector;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Conveyors.Entity.Visual {
    
    [AddComponentMenu("Gameplay/Conveyors/Conveyor Zone Visual")]
    public sealed class ZoneVisual : MonoBehaviour {
        [SerializeField] private List<GameObject> _items;

        private int _currentAmount;

        public void SetupItems(int currentAmount) {
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

        public void IncrementItems(int range) {
            var previousAmount = _currentAmount;
            var newAmount = Mathf.Min(_currentAmount + range, _items.Count);
            _currentAmount = newAmount;

            for (var i = previousAmount; i < newAmount; i++) {
                var item = _items[i];
                item.SetActive(true);
            }
        }

        public void DecrementItems(int range) {
            var previousAmount = _currentAmount;
            var newAmount = Mathf.Max(_currentAmount - range, 0);
            _currentAmount = newAmount;

            for (var i = previousAmount - 1; i >= newAmount; i--) {
                var item = _items[i];
                item.SetActive(false);
            }
        }

#if UNITY_EDITOR
        [Button("Setup Items")]
        private void Editor_SetupItems() {
            _items = new List<GameObject>();
            
            foreach (Transform child in this.transform) {
                _items.Add(child.gameObject);
            }
        }
#endif
    }
}