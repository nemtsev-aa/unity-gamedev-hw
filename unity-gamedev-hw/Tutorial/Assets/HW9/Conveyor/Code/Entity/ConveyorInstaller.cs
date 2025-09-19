using Game.GameEngine.GameResources;
using UnityEngine;
using Zenject;

namespace Conveyors.Entity {

    public sealed class ConveyorInstaller : MonoInstaller {
        [SerializeField] private ConveyourConfig _scriptableConveyour;
        [SerializeField] private ResourceInfoCatalog _resourceCatalog;
        [SerializeField] private ConveyorModel _conveyor;

        public override void InstallBindings() {

            Container.BindInstance(_scriptableConveyour)
                .AsSingle()
                .NonLazy();

            Container.BindInstance(_resourceCatalog)
                .AsSingle()
                .NonLazy();

            Container.BindInstance(_conveyor)
               .AsSingle()
               .NonLazy();
        }
    }
}