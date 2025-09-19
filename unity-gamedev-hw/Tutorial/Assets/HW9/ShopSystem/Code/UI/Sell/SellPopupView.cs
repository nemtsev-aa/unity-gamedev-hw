using ObservableCollections;
using R3;
using ShopSystem.Product.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ShopSystem.UI {

    public sealed class SellPopupView : MonoBehaviour, IDisposable {

        [SerializeField] private Transform _container;
        [SerializeField] private SellProductView _viewPrefab;
        [SerializeField] private Button _hideButton;

        private readonly List<SellProductView> _views = new();
        private readonly CompositeDisposable _disposables = new();
        private ISellPopupViewModel _viewModel;

        public void Init(ISellPopupViewModel viewModel) {
            _viewModel = viewModel;
            gameObject.SetActive(true);

            ClearViews();

            foreach (var productViewModel in _viewModel.ProductPresenters) {
                CreateProductView(productViewModel);
            }

            _hideButton.onClick.AddListener(() => Show(false));

             _viewModel.ProductPresenters
                .ObserveRemove()
                .Subscribe(OnProductRemoved)
                .AddTo(_disposables);

            _viewModel.SellProductCount
                .Subscribe(SellProductCount)
                .AddTo(_disposables);
        }

        public void Show(bool status) {
            gameObject.SetActive(status);
        }

        private void SellProductCount(int currentCount) {
            //Debug.Log($"SellPopupView: ViewCount {currentCount}");
        }

        private void CreateProductView(ISellProductViewModel productViewModel) {
            var productView = Instantiate(_viewPrefab, _container);
            productView.Initialized(productViewModel);
            _views.Add(productView);
        }

        private void OnProductRemoved(CollectionRemoveEvent<ISellProductViewModel> removedItem) {
            var viewToRemove = _views.FirstOrDefault(v => v.ViewModel == removedItem.Value);
            
            if (viewToRemove != null) {
                viewToRemove.Show(false);

                _views.Remove(viewToRemove);
                Destroy(viewToRemove.gameObject);
            }
        }

        private void ClearViews() {
            foreach (var view in _views) {
                view.Dispose();
                Destroy(view.gameObject);
            }
            _views.Clear();
        }

        public void Dispose() {

            ClearViews();
            
            _hideButton.onClick.RemoveListener(() => Show(false));

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();
        }
    }
}

