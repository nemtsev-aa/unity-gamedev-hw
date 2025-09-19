using Zenject;
using UnityEngine;
using Currencies;
using ShopSystem.Installers;

namespace Tutorial.Core {

    public sealed class ShopSystemInstaller : MonoInstaller {
        [SerializeField] private CurrencyInstaller _currency;
        [SerializeField] private ProductCatalogsInstaller _catalogs;

        private HelpersInstaller _helpers = new();

        public override void InstallBindings() {
            _currency.Install(Container);
            _catalogs.Install(Container);
            _helpers.Install(Container);
        }
    }
}