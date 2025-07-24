using ShopSystem.Helpers;
using ShopSystem.Product.Data;
using ShopSystem.Storages;

namespace ShopSystem.Product.UI {

    public sealed class ProductPresenterFactory {
        private readonly IBuyHandler _productBuyer;
        private readonly ProductSeller _productSeller;
        private readonly MoneyStorage _moneyStorage;

        public ProductPresenterFactory(IBuyHandler productBuyer,
                                       ProductSeller productSeller,
                                       MoneyStorage moneyStorage) {

            _productBuyer = productBuyer;
            _productSeller = productSeller;
            _moneyStorage = moneyStorage;
        }

        public SaleProductViewModel CreateSaleProduct(ProductInfo productInfo) {
            return new SaleProductViewModel(productInfo, _productBuyer, _moneyStorage);
        }

        public SellProductViewModel CreateSellProduct(ProductInfo productInfo) {
            return new SellProductViewModel(productInfo, _productSeller, _moneyStorage);
        }
    }
}
