using ObservableCollections;
using R3;
using ShopSystem.Product.Data;
using ShopSystem.Product.UI;
using System;

namespace ShopSystem.UI {

    public sealed class SellPopupViewModel : ISellPopupViewModel, IDisposable {
        private readonly ObservableList<ISellProductViewModel> _productPresenters = new ();

        private readonly Subject<int> _sellProductCount = new();
        private readonly CompositeDisposable _disposables = new();

        public IObservableCollection<ISellProductViewModel> ProductPresenters => _productPresenters;
        public Observable<int> SellProductCount => _sellProductCount;

        public SellPopupViewModel(ProductCatalog productCatalog,
                                  SellCatalog sellCatalog,
                                  ProductPresenterFactory factory) {

            foreach (var (productId, count) in sellCatalog.Products) {

                if (productCatalog.TryGetProductInfo(productId, out var productInfo)) {

                    for (int i = 0; i < count; i++) {
                        var viewModel = factory.CreateSellProduct(productInfo);
                        viewModel.Cleared
                            .Subscribe(OnRemoveProduct)
                            .AddTo(_disposables);

                        _productPresenters.Add(viewModel);
                    }
                }
            }

            _productPresenters.ObserveCountChanged()
                .Subscribe(count => _sellProductCount.OnNext(count))
                .AddTo(_disposables);
        }

        private void OnRemoveProduct(SellProductViewModel product) {
            _productPresenters.Remove(product);
            ProductPresenters.ObserveRemove();
        }

        public void Dispose() => _disposables.Dispose();
    }
}

