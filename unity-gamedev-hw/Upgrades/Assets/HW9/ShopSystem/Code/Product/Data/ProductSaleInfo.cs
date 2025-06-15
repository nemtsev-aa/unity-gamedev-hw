namespace ShopSystem.Product.Data {
    
    public class ProductSaleInfo {
        public ProductInfo Product { get; }
        public int AmountSold { get; }
        public long TotalEarned { get; }
        public long PricePerUnit { get; }

        public ProductSaleInfo(ProductInfo product, int amountSold, long pricePerUnit) {
            Product = product;
            AmountSold = amountSold;
            PricePerUnit = pricePerUnit;
            TotalEarned = pricePerUnit * amountSold;
        }
    }
}
