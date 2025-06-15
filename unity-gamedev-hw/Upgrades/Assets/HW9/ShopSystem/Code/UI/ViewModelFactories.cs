namespace ShopSystem.UI {

    public sealed class ViewModelFactories {
        public ViewModelFactories(ShopPopupViewModelFactory saleFactory,
                                  SellPopupViewModelFactory sellFactory) {

            SaleFactory = saleFactory;
            SellFactory = sellFactory;
        }

        public ShopPopupViewModelFactory SaleFactory { get; private set; }
        public SellPopupViewModelFactory SellFactory { get; private set; }
    }
}
