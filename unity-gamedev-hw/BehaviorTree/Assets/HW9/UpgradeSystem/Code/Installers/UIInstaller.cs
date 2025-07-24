using System;
using Zenject;
using UnityEngine;
using UpgradesSystem.UI;

namespace UpgradesSystem.Installers {

    [Serializable]
    public sealed class UIInstaller {
        [SerializeField] public UpgradesPopupView _upgradesPopup;

        public void Install(DiContainer container) {

            container.Bind<UpgradePresenterFactory>()
                .AsSingle()
                .NonLazy();

            container.Bind<UpgradesPopupViewModelFactory>()
                .AsSingle();

            container.Bind<UICompanents>()
                .AsSingle()
                .WithArguments(_upgradesPopup)
                .NonLazy();
        }
    }
}
