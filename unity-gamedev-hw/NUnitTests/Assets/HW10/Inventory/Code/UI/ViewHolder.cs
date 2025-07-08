namespace InventorySystem.UI {

    public sealed class ViewHolder {
        public readonly InventoryItemView _view;
        public readonly InventoryItemViewPresenter _presenter;

        public ViewHolder(InventoryItemView view, InventoryItemViewPresenter presenter) {
            _view = view;
            _presenter = presenter;
        }
    }
}