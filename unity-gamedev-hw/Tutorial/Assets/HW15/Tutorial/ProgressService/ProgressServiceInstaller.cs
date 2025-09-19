using Zenject;

namespace ProgressService {

    public sealed class ProgressServiceInstaller : MonoInstaller {

        public override void InstallBindings() {

            Container.Bind<Logger>()
                     .AsSingle()
                     .NonLazy();

            Container.Bind<PlayerProgressLoader>()
                     .AsSingle()
                     .NonLazy();

            Container.Bind<Progress>()
                     .AsSingle()
                     .NonLazy();
        }
    }
}



