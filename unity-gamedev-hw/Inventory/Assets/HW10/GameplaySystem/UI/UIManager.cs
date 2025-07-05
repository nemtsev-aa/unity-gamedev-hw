using EquipmentSystem.UI;
using InventorySystem.UI;
using System;
using R3;

namespace GameplaySystem {

    public sealed class UIManager : IDisposable {
        private readonly InventoryPopup _inventoryPopup;
        private readonly InventoryItemInfoPopup _itemInfoPopup;
        private readonly CharacterEquipmentPopup _heroEquipmentPopup;
        private readonly ToolsView _toolsView;

        private CompositeDisposable _disposables = new();

        public UIManager(InventoryPopup inventoryPopup,
                         InventoryItemInfoPopup itemInfoPopup,
                         CharacterEquipmentPopup heroEquipmentPopup,
                         ToolsView toolsView) {

            _inventoryPopup = inventoryPopup;
            _itemInfoPopup = itemInfoPopup;
            _heroEquipmentPopup = heroEquipmentPopup;
            _toolsView = toolsView;

            CreatReactiveSubscribes();
        }

        private void CreatReactiveSubscribes() {

            _toolsView.InventaryButtonClicked
                .Subscribe(OnInventaryButtonClicked)
                .AddTo(_disposables);

            _toolsView.EquipmentButtonClicked
                .Subscribe(OnEquipmentButtonClicked)
                .AddTo(_disposables);
        }

        private void OnInventaryButtonClicked(Unit unit) {
            _inventoryPopup.Show(!_inventoryPopup.gameObject.activeSelf);

            if (_inventoryPopup.gameObject.activeSelf == true) 
                _heroEquipmentPopup.Show(false);
            
        }

        private void OnEquipmentButtonClicked(Unit unit) {
            _heroEquipmentPopup.Show(!_heroEquipmentPopup.gameObject.activeSelf);

            if (_heroEquipmentPopup.gameObject.activeSelf == true)
                _inventoryPopup.Show(false);
        }

        public void Dispose() {

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();
        }
    }
}