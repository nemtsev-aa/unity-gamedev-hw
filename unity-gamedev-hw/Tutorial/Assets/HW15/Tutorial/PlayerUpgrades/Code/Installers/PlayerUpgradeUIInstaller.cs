using System;
using Zenject;
using UnityEngine;
using Tutorial.UI;
using UpgradesSystem.UI;

namespace Tutorial.PlayerUpgrades {

    [Serializable]
    public sealed class PlayerUpgradeUIInstaller {
        [SerializeField] public TutorialCharacterUpgradePopup _upgradesPopup;

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