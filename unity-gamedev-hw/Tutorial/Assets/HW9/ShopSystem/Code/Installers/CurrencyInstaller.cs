using System;
using Zenject;
using UnityEngine;
using Currencies.UI;
using ShopSystem.Storages;

namespace Currencies {

    [Serializable]
    public sealed class CurrencyInstaller {
        [SerializeField] private long _defaultMoneyCount = 11000L;
        [SerializeField] private long _defaultGemCount = 10L;
        [Space, SerializeField] private CurrencyProvider _currencyProvider;

        public void Install(DiContainer container) {
            BindMoney(container);
            BindGem(container);
        }

        private void BindMoney(DiContainer container) {
            container
                .Bind<MoneyStorage>()
                .AsSingle()
                .WithArguments(_defaultMoneyCount)
                .NonLazy();

            container
                .BindInterfacesTo<MoneyPanelAdapter>()
                .AsTransient()
                .WithArguments(_currencyProvider.MoneyView)
                .NonLazy();
        }

        private void BindGem(DiContainer container) {
            container
                .Bind<GemStorage>()
                .AsSingle()
                .WithArguments(_defaultGemCount)
                .NonLazy();

            container.BindInterfacesTo<GemPanelAdapter>()
                .AsSingle()
                .WithArguments(_currencyProvider.GemView)
                .NonLazy();
        }
    }
}
