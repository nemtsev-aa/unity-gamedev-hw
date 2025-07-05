using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace InventorySystem.UI {
    public sealed class SelectableInventoryItemView : InventoryItemView {
        [SerializeField] private Button _button;

        public void AddClickListener(UnityAction action) {
            _button.onClick.AddListener(action);
        }

        public void RemoveClickListener(UnityAction action) {
            _button.onClick.RemoveListener(action);
        }
    }
}