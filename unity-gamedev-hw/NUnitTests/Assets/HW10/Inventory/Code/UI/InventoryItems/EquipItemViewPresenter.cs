using InventorySystem.Core;

namespace InventorySystem.UI {
    public sealed class EquipItemViewPresenter {

        private readonly DraggableInventoryItemView _view;
        private readonly InventoryItem _item;
       
        public EquipItemViewPresenter(DraggableInventoryItemView view, InventoryItem item) {
            _view = view;
            _item = item;
        }

        public void Init() {
            var metadata = _item.Metadata;

            _view.SetItemId(_item.ID);
            _view.SetTitle(metadata.Name);
            _view.SetIcon(metadata.Icon);
        }
    }
}