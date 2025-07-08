using InventorySystem.Core;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventorySystem.UI {

    public sealed class InventoryPopup : MonoBehaviour {
        [SerializeField] private SelectableInventoryItemView _prefab;
        [SerializeField] private Transform _container;

        private IInventoryService _inventoryService;
        private InventoryItemConsumer _consumeManager;
        private InventoryItemInfoPopup _itemInfoPopup;

        private readonly Dictionary<InventoryItem, ViewHolder> _items = new();

        [Inject]
        public void Construct(InventoryService service,
                              InventoryItemInfoPopup itemInfoPopup,
                              InventoryItemConsumer inventoryItemConsumer) {

            _inventoryService = service;
            _itemInfoPopup = itemInfoPopup;
            _consumeManager = inventoryItemConsumer;
        }

        public void Show(bool status) {

            if (status == true) {
                OnShow();
                return;
            }

            OnHide();
        }

        private void OnShow() {

            if (gameObject.activeSelf == false)
                gameObject.SetActive(true);

            var playerInventory = _inventoryService.Inventory;
            playerInventory.ItemAdded += OnAddItem;
            playerInventory.ItemRemoved += OnRemoveItem;

            var inventoryItems = playerInventory.Items;

            for (int i = 0, count = inventoryItems.Count; i < count; i++) {
                var inventoryItem = inventoryItems[i];
                OnAddItem(inventoryItem);
            }
        }

        private void OnHide() {

            if (gameObject.activeSelf == true)
                gameObject.SetActive(false);

            var playerInventory = _inventoryService.Inventory;
            playerInventory.ItemAdded -= OnAddItem;
            playerInventory.ItemRemoved -= OnRemoveItem;

            var inventoryItems = playerInventory.Items;

            for (int i = 0, count = inventoryItems.Count; i < count; i++) {
                var inventoryItem = inventoryItems[i];
                OnRemoveItem(inventoryItem);
            }
        }

        private void OnAddItem(InventoryItem item) {

            if (_items.ContainsKey(item) == true)
                return;

            var view = Instantiate(_prefab, _container);
            var presenter = new InventoryItemViewPresenter(view, item);
            presenter.Init(_itemInfoPopup, _consumeManager);

            var viewHolder = new ViewHolder(view, presenter);
            _items.Add(item, viewHolder);

            presenter.Start();
        }

        private void OnRemoveItem(InventoryItem item) {

            if (_items.ContainsKey(item) == false)
                return;

            var viewHolder = _items[item];
            viewHolder._presenter.Stop();
            Destroy(viewHolder._view.gameObject);

            _items.Remove(item);
        }
    }
}