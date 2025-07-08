using UnityEngine;
using UnityEngine.EventSystems;
using InventorySystem.Core;
using InventorySystem.ItemComponents;
using EquipmentSystem.Core;
using EquipmentSystem.UI;
using InventorySystem.UI;
using System;

namespace EquipmentSystem.UI {

    public sealed class DragDropLogicController {

        public event Action<EquipSlotTypes> DragStarted;
        public event Action DropComplited;

        private readonly IEquipmentService _equipmentService;
        private readonly IInventoryService _inventoryService;
        private readonly DragDropVisualController _visualController;

        private InventoryItemView _draggedView;
        private InventoryItem _draggedItem;

        public DragDropLogicController(
            IEquipmentService equipmentService,
            IInventoryService inventoryService,
            DragDropVisualController visualController) {

            _equipmentService = equipmentService;
            _inventoryService = inventoryService;
            _visualController = visualController;
        }

        public void OnBeginDrag(DraggableInventoryItemView itemView, PointerEventData eventData) {
            _draggedView = itemView;

            if (_inventoryService.Inventory.TryFindItem(itemView.ID, out _draggedItem) == false||
                _draggedItem.TryGetComponent<EquippableItemComponent>(out _) == false) {
                
                _draggedView = null;
                _draggedItem = null;

                return;
            }

            _visualController.StartDrag(_draggedItem.Metadata.Icon, eventData.position);

            if (_draggedItem.TryGetComponent<EquippableItemComponent>(out var companent) == true) {
                DragStarted?.Invoke(companent.Type);
            }
        }

        public void OnDrag(PointerEventData eventData) {
            _visualController.UpdateDragPosition(eventData.delta);

            bool checkResult = CheckEquipmentSlot(eventData, out EquipmentSlotView slotView);

            if (slotView != null) {

                if (checkResult == false)
                    slotView.StopHighlightAnimation(Color.red);
            }
        }

        public void OnEndDrag(PointerEventData eventData) {
            
            try {
                TryHandleDrop(eventData);
            }
            finally {
                _visualController.EndDrag();
                DropComplited?.Invoke();
            }
        }

        private bool CheckEquipmentSlot(PointerEventData eventData, out EquipmentSlotView view) {

            if (eventData.pointerEnter == null) {
                view = default;
                return false;
            }

            var viewTransform = eventData.pointerEnter.transform.parent;

            if (viewTransform.TryGetComponent(out EquipmentSlotView slotView) == false) {
                view = default;
                return false;
            }

            if (_draggedItem.TryGetComponent(out EquippableItemComponent equipComponent) == false) {
                view = default;
                return false;
            }
 
            if (equipComponent.Type != slotView.SlotType) {
                view = slotView;
                return false;
            }

            view = slotView;
            return true;
        }

        private bool TryHandleDrop(PointerEventData eventData) {
            
            if (CheckEquipmentSlot(eventData, out EquipmentSlotView slotView) == true) {
                return _equipmentService.TryEquipItem(_draggedItem, slotView.Index);
            }

            return false;
        }
    }
}
