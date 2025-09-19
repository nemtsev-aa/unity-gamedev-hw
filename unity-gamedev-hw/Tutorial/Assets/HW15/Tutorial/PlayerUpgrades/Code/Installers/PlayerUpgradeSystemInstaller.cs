using Zenject;
using UnityEngine;
using UpgradesSystem.Core;

namespace Tutorial.PlayerUpgrades {

    public sealed class PlayerUpgradeSystemInstaller : MonoInstaller {
        [SerializeField] private UpgradeCatalog _catalog;
        [SerializeField] private PlayerUpgradeUIInstaller _ui;
        
        public override void InstallBindings() {

            Container.BindInstance(_catalog)
                     .AsSingle()
                     .NonLazy();

            Container.Bind<IUpgradeSystem>()
                     .To<PlayerUpgradeSystem>()
                     .AsSingle()
                     .NonLazy();

            _ui.Install(Container);
        }
    }
}