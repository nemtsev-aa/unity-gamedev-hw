using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InventorySystem.UI {

    [RequireComponent(typeof(CanvasGroup), typeof(EventTrigger))]
    public sealed class DraggableInventoryItemView : InventoryItemView {
        
        public event Action<DraggableInventoryItemView, PointerEventData> OnStartDrag;
        public event Action<PointerEventData> OnDuringDrag;
        public event Action<PointerEventData> OnEndDrag;

        public CanvasGroup CanvasGroup => _canvasGroup;

        private CanvasGroup _canvasGroup;
        private EventTrigger _eventTrigger;

        private void Awake() {
            _canvasGroup = transform.GetComponent<CanvasGroup>();
            _eventTrigger = transform.GetComponent<EventTrigger>();
            
            InitializeDragDrop();
        }

        private void InitializeDragDrop() {
            EventTrigger.Entry beginDragEntry = new EventTrigger.Entry {
                eventID = EventTriggerType.BeginDrag
            };

            beginDragEntry.callback.AddListener((data) => {
                OnStartDrag?.Invoke(this, (PointerEventData)data);
            });

            EventTrigger.Entry dragEntry = new EventTrigger.Entry {
                eventID = EventTriggerType.Drag
            };

            dragEntry.callback.AddListener((data) => {
                OnDuringDrag?.Invoke((PointerEventData)data);
            });

            EventTrigger.Entry endDragEntry = new EventTrigger.Entry {
                eventID = EventTriggerType.EndDrag
            };

            endDragEntry.callback.AddListener((data) => {
                OnEndDrag?.Invoke((PointerEventData)data);
            });

            _eventTrigger.triggers.Add(beginDragEntry);
            _eventTrigger.triggers.Add(dragEntry);
            _eventTrigger.triggers.Add(endDragEntry);
        }
    }
}