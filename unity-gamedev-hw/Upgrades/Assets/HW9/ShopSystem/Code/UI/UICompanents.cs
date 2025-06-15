namespace ShopSystem.UI {

    public sealed class UICompanents {
        public UICompanents(PopupProvider popupProvider,
                            ShopPopupViewModelFactory saleFactory,
                            SellPopupViewModelFactory sellFactory) {

            PopupProvider = popupProvider;
            ViewModelFactories = new ViewModelFactories(saleFactory, sellFactory);
        }

        public PopupProvider PopupProvider { get; }
        public ViewModelFactories ViewModelFactories { get; }
    }
}
