using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using EquipmentSystem.Core;
using InventorySystem.Core;

namespace EquipmentSystem.UI {

    public sealed class EquipmentSlotView : MonoBehaviour,
                                            IDropHandler {

        public const float ANIMATION_DURATION = 1f;

        public event Action<EquipmentSlotView> ItemDrop;
        public int Index => _data.Index;
        public EquipSlotTypes SlotType => _data.Type;
        public InventoryItem CurrentItem => _currentItem;

        [SerializeField] private Image _iconImage;
        [SerializeField] private Image _highlight;

        private EquipmentSlot _data;
        private InventoryItem _currentItem;
        private Tween _highlightAnimation;

        public void Init(EquipmentSlot data) {
            _data = data;
            _iconImage.gameObject.SetActive(false);
        }

        public void SetItem(InventoryItem item) {
            _currentItem = item;

            _iconImage.sprite = _currentItem?.Metadata.Icon;
            _iconImage.gameObject.SetActive(_currentItem != null);

            UpdateHighlight(_currentItem != null ? Color.white : Color.clear);
        }

        public void StartHighlightAnimation(Color color) {
            _highlight.gameObject.SetActive(true);
            _highlight.color = color;

            _highlightAnimation?.Kill();
            _highlightAnimation = _highlight.DOFade(0.2f, ANIMATION_DURATION)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.Linear);
        }

        public void StopHighlightAnimation(Color finalColor) {
            _highlightAnimation?.Kill();
            _highlightAnimation = null;

            UpdateHighlight(finalColor);
        }

        private void UpdateHighlight(Color color) {
            _highlight.color = color;
            _highlight.gameObject.SetActive(color.a > 0.1f);
        }

        public void OnDrop(PointerEventData eventData) {
            ItemDrop?.Invoke(this);
        }
    }
}
