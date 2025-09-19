using UnityEngine;
using Zenject;

namespace HintPlayerControlService {
    public sealed class HintPlayerControlServiceInstaller : MonoInstaller {
        [SerializeField] private HintPlayerControlConfigs _config;
        [SerializeField] private HintPlayerControlView _view;

        public override void InstallBindings() {
            Container.BindInstance(_config)
                     .AsSingle()
                     .NonLazy();

            Container.Bind<IHintPlayerControlViewModel>()
                     .To<HintPlayerControlViewModel>()
                     .AsSingle()
                     .NonLazy();

            Container.BindInstance(_view)
                     .AsSingle()
                     .NonLazy();
        }
    }
}