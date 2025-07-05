using UnityEngine;
using UnityEngine.UI;

namespace EquipmentSystem.UI {

    public sealed class DragDropVisualController : MonoBehaviour {

        private float DRAG_ICON_SIZE = 50f;
        private Canvas _canvas;
        private Image _dragIcon;

        public void Initialize(Canvas canvas, Image dragIconPrefab) {
            _canvas = canvas;
            _dragIcon = Instantiate(dragIconPrefab, _canvas.transform);
            _dragIcon.gameObject.SetActive(false);
        }

        public void StartDrag(Sprite icon, Vector2 position) {
            _dragIcon.sprite = icon;
            _dragIcon.rectTransform.sizeDelta = Vector2.one * DRAG_ICON_SIZE;
            _dragIcon.transform.position = position;
            _dragIcon.gameObject.SetActive(true);
            _dragIcon.raycastTarget = false;
        }

        public void UpdateDragPosition(Vector2 delta) {
            _dragIcon.rectTransform.anchoredPosition += delta / _canvas.scaleFactor;
        }

        public void EndDrag() {
            _dragIcon.gameObject.SetActive(false);
        }
    }
}
