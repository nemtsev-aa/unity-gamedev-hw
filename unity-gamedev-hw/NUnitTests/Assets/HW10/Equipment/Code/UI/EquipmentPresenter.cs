using InventorySystem.Core;
using InventorySystem.ItemComponents;
using InventorySystem.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace EquipmentSystem.UI {

    public sealed class EquipmentPresenter : MonoBehaviour {
        [SerializeField] private DraggableInventoryItemView _prefab;
        [SerializeField] private RectTransform _container;

        private List<DraggableInventoryItemView> _draggableItems = new List<DraggableInventoryItemView>();

        private IInventory _inventory;
        private DragDropLogicController _dragDropLogicController;

        [Inject]
        public void Construct(IInventoryService inventoryService,
                              DragDropLogicController dragDropLogicController) {

            _inventory = inventoryService.Inventory;
            _dragDropLogicController = dragDropLogicController;

            _inventory.ItemAdded += Inventory_ItemAdded;
            _inventory.ItemRemoved += Inventory_ItemRemoved;
        }

        public void Show(bool status) {

            gameObject.SetActive(status);

            if (status == false)
                return;
        }

        private void CreateDraggableItems() {

            foreach (var item in _inventory.Items) {

                if (TryCreateDraggableItemView(item, out DraggableInventoryItemView view) == true)
                    _draggableItems.Add(view);
            }
        }

        private bool TryCreateDraggableItemView(InventoryItem item, out DraggableInventoryItemView itemView) {

            if (item.TryGetComponent<EquippableItemComponent>(out _) == false ||
                item.Flags.HasFlag(InventoryItemFlags.EQUPPABLE) == false) {

                itemView = default;
                return false;
            }

            itemView = Instantiate(_prefab, _container);
            itemView.Stack.gameObject.SetActive(false);

            var presenter = new EquipItemViewPresenter(itemView, item);
            presenter.Init();

            itemView.OnStartDrag += Item_OnStartDrag;
            itemView.OnDuringDrag += Item_OnDuringDrag;
            itemView.OnEndDrag += Item_OnEndDrag;

            return itemView;
        }

        private void Item_OnStartDrag(DraggableInventoryItemView view, PointerEventData data) {
            _dragDropLogicController.OnBeginDrag(view, data);
        }

        private void Item_OnDuringDrag(PointerEventData data) {
            _dragDropLogicController.OnDrag(data);
        }

        private void Item_OnEndDrag(PointerEventData data) {
            _dragDropLogicController.OnEndDrag(data);
        }

        private void Inventory_ItemAdded(InventoryItem item) {

            if (TryCreateDraggableItemView(item, out DraggableInventoryItemView view) == true)
                _draggableItems.Add(view);
        }

        private void Inventory_ItemRemoved(InventoryItem item) {

            var view = _draggableItems.FirstOrDefault(i => i.ID == item.ID);

            if (view != null) {
                Destroy(view.gameObject);
                _draggableItems.Remove(view);
            }
        }
    }
}
