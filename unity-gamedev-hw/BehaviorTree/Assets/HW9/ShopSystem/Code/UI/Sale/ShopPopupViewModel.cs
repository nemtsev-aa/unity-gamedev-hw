using ObservableCollections;
using ShopSystem.Product.Data;
using ShopSystem.Product.UI;

namespace ShopSystem.UI {
    public sealed class ShopPopupViewModel : IShopPopupViewModel {
        private readonly ObservableList<ISaleProductViewModel> _productPresenters = new();

        public IReadOnlyObservableList<ISaleProductViewModel> ProductPresenters => _productPresenters;

        public ShopPopupViewModel(ProductCatalog catalog, ProductPresenterFactory factory) {

            for (var index = 0; index < catalog.Products.Count; index++) {
                ProductInfo product = catalog.Products[index];
                SaleProductViewModel viewModel = factory.CreateSaleProduct(product);

                _productPresenters.Add(viewModel);
            }
        }

        public ShopPopupViewModel(ProductInfo product, ProductPresenterFactory factory) {
            SaleProductViewModel viewModel = factory.CreateSaleProduct(product);

            _productPresenters.Add(viewModel);
        }
    }
}
