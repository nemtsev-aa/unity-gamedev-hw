using Zenject;

namespace SaveLoadSystem {

    namespace Core {

        public sealed class ZenjectContext : IContext {
            private DiContainer _diContainer;

            public ZenjectContext(DiContainer diContainer) {
                _diContainer = diContainer;
            }

            public TService GetService<TService>() {
                return _diContainer.Resolve<TService>();
            }
        }
    }
}

