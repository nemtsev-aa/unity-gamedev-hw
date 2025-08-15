using Zenject;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;

namespace AssetManagementSystem {

    public sealed class AssetManagementSystemInstaller : MonoInstaller {
        [SerializeField] private List<AssetReference> _criticalAssets;

        public override void InstallBindings() {

            Container.Bind<AssetManager>()
                .AsSingle()
                .NonLazy();

            Container.Bind<AssetPreloader>()
                .AsSingle()
                .WithArguments(_criticalAssets)
                .NonLazy();
        }
    }
}
