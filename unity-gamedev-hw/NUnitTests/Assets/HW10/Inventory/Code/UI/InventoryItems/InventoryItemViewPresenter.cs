using InventorySystem.Core;
using InventorySystem.ItemComponents;

namespace InventorySystem.UI {

    public sealed class InventoryItemViewPresenter {

        private readonly SelectableInventoryItemView _view;
        private readonly InventoryItem _item;
        private StackableItemComponent _stackableComponent;
        private InventoryItemInfoPopup _itemInfoPopup;
        private InventoryItemConsumer _consumeManager;

        public InventoryItemViewPresenter(SelectableInventoryItemView view, InventoryItem item) {
            _view = view;
            _item = item;
        }

        public void Init(InventoryItemInfoPopup itemInfoPopup, InventoryItemConsumer consumeManager) {
            _itemInfoPopup = itemInfoPopup;
            _consumeManager = consumeManager;
        }

        public void Start() {
            var metadata = _item.Metadata;

            _view.SetItemId(_item.ID);
            _view.SetTitle(metadata.Name);
            _view.SetIcon(metadata.Icon);

            var flagsExists = _item.Flags.HasFlag(InventoryItemFlags.STACKABLE);
            _view.Stack.SetVisible(flagsExists);

            if (flagsExists == true) {

                if (_item.TryGetComponent(out _stackableComponent) == true) {
                    _stackableComponent.OnValueChanged += OnAmountChanged;

                    _view.Stack.SetAmount(_stackableComponent.Count, _stackableComponent.MaxCount);
                }
            }

            _view.AddClickListener(OnItemClicked);
        }

        public void Stop() {
            if (_item.Flags.HasFlag(InventoryItemFlags.STACKABLE) == true)
                _stackableComponent.OnValueChanged -= OnAmountChanged;

            _view.RemoveClickListener(OnItemClicked);
        }

        private void OnAmountChanged(int newCount) {
            _view.Stack.SetAmount(newCount, _stackableComponent.MaxCount);
        }

        private void OnItemClicked() {
            var presenter = new InventoryItemPresentationModel(_item, _consumeManager);
            _itemInfoPopup.Init(presenter);
        }
    }
}