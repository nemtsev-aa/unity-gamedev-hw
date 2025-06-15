using R3;
using System;
using UnityEngine;
using ShopSystem.Product.Data;
using ShopSystem.Storages;

namespace ShopSystem.Helpers {

    public sealed class ProductBuyer : IBuyHandler, IDisposable {
        public ReadOnlyReactiveProperty<long> Money => _money;
        public Subject<ProductInfo> ProductPurchased = new Subject<ProductInfo>();

        private readonly ReactiveProperty<long> _money;
        private readonly MoneyStorage _moneyStorage;
        private readonly CompositeDisposable _disposable = new();

        public ProductBuyer(MoneyStorage moneyStorage) {
            _moneyStorage = moneyStorage;
            _money = new ReactiveProperty<long>(_moneyStorage.Money.CurrentValue);
            _moneyStorage.Money
                .Subscribe(OnMoneyChange)
                .AddTo(_disposable);
        }

        private void OnMoneyChange(long money) {
            _money.Value = money;
        }

        public void Buy(ProductInfo product) {

            if (CanBuy(product) == true) {
                _moneyStorage.SpendMoney(product.MoneyPrice);
                ProductPurchased.OnNext(product);

                Debug.Log($"<color=green>Product {product.Title} successfully purchased!</color>");
                return;
            }

            Debug.LogWarning($"<color=red>Not enough money for product {product.Title}!</color>");
        }

        public bool CanBuy(ProductInfo product) {
            return _moneyStorage.Money.CurrentValue >= product.MoneyPrice;
        }

        public void Dispose() {

            if (_disposable.IsDisposed == false)
                _disposable.Dispose();
        }
    }
}
