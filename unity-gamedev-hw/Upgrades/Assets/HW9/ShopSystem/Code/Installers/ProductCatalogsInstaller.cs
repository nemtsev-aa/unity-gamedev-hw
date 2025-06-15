using System;
using Zenject;
using UnityEngine;
using ShopSystem.Product.Data;

namespace Currencies {

    [Serializable]
    public sealed class ProductCatalogsInstaller  {
        [SerializeField] private ProductCatalog _productCatalog;

        public void Install(DiContainer container) {
            container.BindInstance(_productCatalog)
                .AsSingle()
                .NonLazy();

            container.Bind<SellCatalog>()
               .AsSingle()
               .NonLazy();
        }
    }
}
