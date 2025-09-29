using System;
using Zenject;
using UnityEngine;
using Client.Installer;

namespace GameCycleSystem {

    [Serializable]
    public sealed class UnitManagerInstaller : MonoInstaller {
        [SerializeField] private UnitBaseConfigs _unitBaseConfigs;
        [SerializeField] private UnitManagerConfig _config;
        [SerializeField] private UnitSpawnPointPresenter _unitSpawnPoint;

        public override void InstallBindings() {
            Container.BindInstance(_unitBaseConfigs)
                .AsSingle();

            Container.BindInstance(_config)
                .AsSingle();

            Container.BindInstance(_unitSpawnPoint)
               .AsSingle();

            Container.Bind<UnitManager>()
                .AsSingle()
                .NonLazy();
        }
    }
}
