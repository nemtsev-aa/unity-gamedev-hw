using Zenject;
using UnityEngine;
using Currencies;

namespace ShopSystem.Installers {

    public sealed class ShopSystemInstaller : MonoInstaller {
        [SerializeField] private ProductCatalogsInstaller _catalogs;
        [SerializeField] private CurrencyInstaller _currency;
        [SerializeField] private UIInstaller _ui;

        private HelpersInstaller _helpers = new();

        public override void InstallBindings() {
            _catalogs.Install(Container);
            _currency.Install(Container);
            _helpers.Install(Container);
            _ui.Install(Container);
        }
    }
}
