using ShopSystem.Product.Data;

namespace ShopSystem.Helpers {
    public interface IBuyHandler {
        void Buy(ProductInfo product);
    }
}
