using Zenject;
using UnityEngine;

namespace SaveSystem {

    public sealed class SaveSystemInstaller : MonoInstaller {
        [SerializeField] private SaveManagerConfig _config;

        public override void InstallBindings() {

            Container.BindInstance(_config)
                     .AsSingle()
                     .NonLazy();

            Container.Bind<ISaveManager>()
                     .To<SavesManager>()
                     .AsSingle()
                     .NonLazy();
        }
    }
}

