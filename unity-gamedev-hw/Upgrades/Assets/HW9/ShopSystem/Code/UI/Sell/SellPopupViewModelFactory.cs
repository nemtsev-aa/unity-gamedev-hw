using ShopSystem.Product.UI;
using ShopSystem.Product.Data;

namespace ShopSystem.UI {
    public sealed class SellPopupViewModelFactory {
        private readonly ProductCatalog _productCatalog;
        private readonly SellCatalog _sellCatalog;
        private readonly ProductPresenterFactory _presenterFactory;

        public SellPopupViewModelFactory(ProductCatalog productCatalog,
                                         SellCatalog sellCatalog,
                                         ProductPresenterFactory presenterFactory) {

            _productCatalog = productCatalog;
            _sellCatalog = sellCatalog;
            _presenterFactory = presenterFactory;
        }

        public SellPopupViewModel Get() {
            return new SellPopupViewModel(_productCatalog, _sellCatalog, _presenterFactory);
        }
    }
}
