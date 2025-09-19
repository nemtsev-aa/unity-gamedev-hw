using Zenject;

namespace InputService {
    public sealed class InputServiceInstaller : MonoInstaller {
        public override void InstallBindings() {
            Container.BindInterfacesAndSelfTo<InputController>()
                    .AsSingle()
                    .NonLazy();

            Container.Bind<InputHandler>()
                    .AsSingle()
                    .NonLazy();
        }
    }
}

