using InputService;
using UnityEngine;
using Zenject;

namespace CursorChangeService {

    public sealed class CursorChangeServiceInstaller : MonoInstaller {
        [SerializeField] private CursorChangeServiceConfig _config;

        public override void InstallBindings() {

            var inputController = Container.Resolve<InputController>();
            var raycasterViewModel = new CursorChangeRaycasterViewModel(_config);
            var raycaster = new CursorChangeRaycaster(inputController, raycasterViewModel);

            var cursorChangeView = new CursorChangeView();
            var cursorChangeViewModel = new CursorChangeViewModel(_config);
            cursorChangeView.Init(cursorChangeViewModel);

            Container.BindInstance(raycaster)
                     .AsSingle()
                     .NonLazy();

            Container.Bind<CursorChangeMediator>()
                     .AsSingle()
                     .WithArguments(raycaster, cursorChangeView)
                     .NonLazy();
        }
    }
}

