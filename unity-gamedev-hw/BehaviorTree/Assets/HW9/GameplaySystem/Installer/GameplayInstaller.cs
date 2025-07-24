using Zenject;
using UnityEngine;

namespace GameplaySystem {

    public sealed class GameplayInstaller : MonoInstaller {
        [SerializeField] private ToolsView _toolsView;

        public override void InstallBindings() {

            Container.BindInterfacesAndSelfTo<ConveyorSupplier>()
                .AsSingle()
                .NonLazy();

            Container.Bind<GameplayMediator>()
                .AsSingle()
                .NonLazy();

            Container.Bind<UIManager>()
                .AsSingle()
                .WithArguments(_toolsView)
                .NonLazy();
        }
    }
}