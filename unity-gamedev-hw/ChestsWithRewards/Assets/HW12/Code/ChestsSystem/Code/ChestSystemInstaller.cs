using Zenject;
using UnityEngine;

namespace ChestsSystem {

    public sealed class ChestSystemInstaller : MonoInstaller {
        [SerializeField] private ChestSystemConfig _config;
        [SerializeField] private ChestsPopup _chestsPopup;

        public override void InstallBindings() {

            Container.BindInstance(_config)
                .AsSingle()
                .NonLazy();

            Container.BindInstance(_chestsPopup)
                .AsSingle()
                .NonLazy();

            Container.Bind<ChestSystem>()
                .AsSingle()
                .NonLazy();

            Container.Bind<ViewModelFactory>()
                .AsSingle()
                .NonLazy(); 

            Container.Bind<ChestsSystem_UI>()
                .AsSingle()
                .NonLazy();
        }
    }
}
