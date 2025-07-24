using R3;
using Elementary;
using UnityEngine;
using Conveyors.Entity;
using ShopSystem.Helpers;
using ShopSystem.Storages;
using ShopSystem.Product.Data;

namespace GameplaySystem {

    public sealed class ConveyorSupplier : IBuyHandler {

        public Subject<ProductInfo> ProductPurchased = new Subject<ProductInfo>();

        private readonly MoneyStorage _moneyStorage;
        private readonly IntVariableLimited _loadStorage;
        
        public ConveyorSupplier(MoneyStorage moneyStorage, ConveyorModel conveyor) {
            _moneyStorage = moneyStorage;
            _loadStorage = conveyor.Core.LoadStorage;
        }

        public void Buy(ProductInfo product) {

            if (CanBuy(product) == true) {
                _moneyStorage.SpendMoney(product.MoneyPrice);
                ProductPurchased.OnNext(product);

                Debug.Log($"<color=green>Product {product.Title} successfully purchased!</color>");
                return;
            }
        }

        private bool CanBuy(ProductInfo product) {
            bool moneyCondition = _moneyStorage.Money.CurrentValue >= product.MoneyPrice;
            bool loadingLimitCondition = _loadStorage.IsLimit;

            if (moneyCondition == false) {
                Debug.LogWarning($"<color=red>Not enough money for product {product.Title}!</color>");
                return false;
            }
            
            if (loadingLimitCondition == true) {
                Debug.LogWarning($"<color=red>LoadZone limit has been reached!</color>");
                return false;
            }

            return true;
        }
    }
}