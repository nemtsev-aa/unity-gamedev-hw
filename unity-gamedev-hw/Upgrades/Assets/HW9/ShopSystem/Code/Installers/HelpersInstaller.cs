using ShopSystem.Helpers;
using System;
using Zenject;

namespace ShopSystem.Installers {

    [Serializable]
    public sealed class HelpersInstaller {

        public void Install(DiContainer container) {
            //container.Bind<ProductBuyer>()
            //    .AsSingle()
            //    .NonLazy();

            container.Bind<ProductSeller>()
                .AsSingle()
                .NonLazy();
        }
    }
}
