namespace ShopSystem.Helpers {

    using R3;
    using ShopSystem.Product.Data;
    using ShopSystem.Storages;
    using System;
    using UnityEngine;

    public sealed class ProductSeller : IDisposable {
        public ReadOnlyReactiveProperty<long> TotalEarnings => _totalEarnings;
        public Observable<ProductSaleInfo> ProductSold => _productSold;

        private readonly ReactiveProperty<long> _totalEarnings = new ReactiveProperty<long>();
        private readonly Subject<ProductSaleInfo> _productSold = new Subject<ProductSaleInfo>();
        private readonly SellCatalog _sellCatalog;
        private readonly MoneyStorage _moneyStorage;
        private readonly ProductCatalog _productCatalog;
        private readonly IDisposable _catalogDisposable;

        public ProductSeller(SellCatalog sellCatalog, ProductCatalog productCatalog, MoneyStorage moneyStorage) {
            _sellCatalog = sellCatalog ?? throw new ArgumentNullException(nameof(sellCatalog));
            _moneyStorage = moneyStorage ?? throw new ArgumentNullException(nameof(moneyStorage));
            _productCatalog = productCatalog ?? throw new ArgumentNullException(nameof(productCatalog));

            // Можно добавить подписку на изменения каталога при необходимости
            _catalogDisposable = Disposable.Empty;
        }

        /// <summary>
        /// Пытается продать указанный товар
        /// </summary>
        /// <param name="productId">ID товара</param>
        /// <param name="sellPrice">Цена продажи (если null - используется базовая цена из ProductInfo)</param>
        /// <returns>True если продажа успешна</returns>
        public bool TrySellProduct(string productId, int amountSold = 1, long? sellPrice = null) {
            if (_sellCatalog.ContainsProduct(productId) == false) {
                Debug.LogWarning($"<color=orange>Product {productId} not found in catalog!</color>");
                return false;
            }

            var productInfo = GetProductInfo(productId); // Получаем информацию о товаре
            if (productInfo == null) {
                Debug.LogError($"<color=red>Product info for {productId} not found!</color>");
                return false;
            }

            var actualPrice = sellPrice ?? productInfo.MoneyPrice;

            // Удаляем товар из каталога
            if (_sellCatalog.TryRemoveProduct(productId, amountSold) == true) {
                // Добавляем деньги
                _moneyStorage.AddMoney(actualPrice * amountSold);
                _totalEarnings.Value += actualPrice * amountSold;

                // Уведомляем о продаже
                var saleInfo = new ProductSaleInfo(productInfo, amountSold, actualPrice);
                _productSold.OnNext(saleInfo);

                Debug.Log($"<color=green>Sold {amountSold}x {productInfo.Title} for {actualPrice} each (total: {actualPrice * amountSold})</color>");
                return true;
            }

            Debug.Log($"<color=red>Sold {amountSold}x {productInfo.Title} for {actualPrice} each (total: {actualPrice * amountSold})</color>");
            return false;
        }

        /// <summary>
        /// Проверяет возможность продажи товара
        /// </summary>
        public bool CanSellProduct(string productId) {
            return _sellCatalog.ContainsProduct(productId);
        }

        /// <summary>
        /// Получает количество доступного товара
        /// </summary>
        public int GetProductAmount(string productId) {
            return _sellCatalog.GetProductAmount(productId);
        }

        public void Dispose() {
            _catalogDisposable?.Dispose();
            _productSold?.OnCompleted();
            _productSold?.Dispose();
        }

        private ProductInfo GetProductInfo(string productId) {
            return _productCatalog.GetProductInfo(productId);
        }
    }
}
