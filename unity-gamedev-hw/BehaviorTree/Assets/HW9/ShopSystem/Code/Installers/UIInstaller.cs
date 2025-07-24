using System;
using Zenject;
using UnityEngine;
using ShopSystem.UI;
using ShopSystem.Product.UI;

namespace ShopSystem.Installers {

    [Serializable]
    public sealed class UIInstaller {
        [SerializeField] public PopupProvider _popupProvider;

        public void Install(DiContainer container) {

            container.Bind<ProductPresenterFactory>()
                .AsSingle();

            container.Bind<ShopPopupViewModelFactory>()
                .AsSingle();

            container.Bind<SellPopupViewModelFactory>()
                .AsSingle();

            container.Bind<UICompanents>()
                .AsSingle()
                .WithArguments(_popupProvider)
                .NonLazy();
        }
    }
}
