using R3;
using System;
using UnityEngine;
using Conveyors.Entity;
using ShopSystem.Helpers;
using ShopSystem.Product.Data;

namespace GameplaySystem {

    public sealed class GameplayMediator : IDisposable {
        private readonly ConveyorSupplier _supplier;
        private readonly ProductSeller _productSeller;
        private readonly SellCatalog _sellCatalog;
        private readonly ConveyorModel _conveyor;

        private readonly string _inputResourceType;

        private CompositeDisposable _disposables = new();

        private GameplayMediator(ConveyorSupplier supplier,
                                 ProductSeller productSeller,
                                 SellCatalog sellCatalog,
                                 ConveyorModel conveyor) {

            _supplier = supplier;
            _productSeller = productSeller;

            _sellCatalog = sellCatalog;
            _conveyor = conveyor;
            _inputResourceType = _conveyor.Config.InputResourceType.ToString();

            CreateReactiveSubscribes();
        }

        private void CreateReactiveSubscribes() {
            _supplier.ProductPurchased
                .Subscribe(OnProductPurchased)
                .AddTo(_disposables);

            _productSeller.ProductSold
                .Subscribe(OnProductSold)
                .AddTo(_disposables);

            _conveyor.Core.UnloadStorage.OnValueChanged += UnloadStorage_ValueChanged;
        }

        private void OnProductPurchased(ProductInfo info) {

            if (info == null)
                return;

            if (info.ID != _inputResourceType || TryPutResource() == false) {
                Debug.LogError($"<color=red>{info.Title} not added to the conveyor!</color>");
                return;
            }

            Debug.Log($"<color=green>{info.Title} successfully added to the conveyor!</color>");
        }

        private bool TryPutResource(int amount = 1) {
            if (_conveyor.Core.LoadStorage.Current + amount > _conveyor.Config.InputCapacity)
                return false;

            _conveyor.Core.LoadStorage.Current += amount;
            return true;
        }

        private void OnProductSold(ProductSaleInfo info) {
            _conveyor.Core.UnloadStorage.Current -= 1;
        }

        private void UnloadStorage_ValueChanged(int currentCount) {

            string outputResourceId = _conveyor.Config.OutputResourceType.ToString();
            if (_sellCatalog.GetProductAmount(outputResourceId) == currentCount)
                return;

            _sellCatalog.AddProduct(outputResourceId, 1);
        }

        public void Dispose() {

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();

            _conveyor.Core.UnloadStorage.OnValueChanged -= UnloadStorage_ValueChanged;
        }
    }
}