using ShopSystem.Product.Data;
using ShopSystem.Product.UI;

namespace ShopSystem.UI {
    public sealed class ShopPopupViewModelFactory {
        private readonly ProductCatalog _productCatalog;
        private readonly ProductPresenterFactory _presenterFactory;

        public ShopPopupViewModelFactory(ProductCatalog productCatalog, ProductPresenterFactory presenterFactory) {
            _productCatalog = productCatalog;
            _presenterFactory = presenterFactory;
        }

        public ShopPopupViewModel Get() {
            return new ShopPopupViewModel(_productCatalog, _presenterFactory);
        }

        public ShopPopupViewModel Get(int productIndex) {
            return new ShopPopupViewModel(_productCatalog.Products[productIndex], _presenterFactory);
        }
    }
}
