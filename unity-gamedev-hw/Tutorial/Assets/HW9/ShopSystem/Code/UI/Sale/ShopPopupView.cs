using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShopSystem.UI {

    public sealed class ShopPopupView : MonoBehaviour, IDisposable {
        [SerializeField] private Transform _container;
        [SerializeField] private ProductView _viewPrefab;
        [SerializeField] private Button _hideButton;

        private readonly List<ProductView> _views = new();

        public void Init(IShopPopupViewModel shopPopupViewModel) {
           
            for (var index = 0; index < shopPopupViewModel.ProductPresenters.Count; index++) {
                var productViewModel = shopPopupViewModel.ProductPresenters[index];
                var productView = Instantiate(_viewPrefab, _container);
                productView.Initialized(productViewModel);

                _views.Add(productView);
            }

            _hideButton.onClick.AddListener( () => Show(false));
        }

        public void Show(bool status) {
            gameObject.SetActive(status);
        }

        public void Dispose() {

            for (var index = 0; index < _views.Count; index++) {
                ProductView productView = _views[index];
                productView.Dispose();

                Destroy(productView.gameObject);
            }

            _views.Clear();
            _hideButton.onClick.RemoveListener(() => Show(false));
        }
    }
}
