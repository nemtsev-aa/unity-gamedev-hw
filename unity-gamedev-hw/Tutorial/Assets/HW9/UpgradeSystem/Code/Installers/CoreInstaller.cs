using System;
using Zenject;
using UnityEngine;
using UpgradesSystem.Core;

namespace UpgradesSystem.Installers {

    [Serializable]
    public sealed class CoreInstaller {
        [SerializeField] private UpgradeCatalog _catalog;

        public void Install(DiContainer container) {

            container.BindInstance(_catalog)
                     .AsSingle()
                     .NonLazy();

            container.Bind<IUpgradeSystem>()
                     .To<UpgradeSystem>()
                     .AsSingle()
                     .NonLazy();
        }
    }
}
