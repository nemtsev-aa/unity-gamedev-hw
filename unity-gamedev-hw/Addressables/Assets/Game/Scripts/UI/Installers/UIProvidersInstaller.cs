using System;
using Zenject;

namespace UI.Core {

    [Serializable]
    public sealed class UIProvidersInstaller {

        public void Install(DiContainer container) {

            container.Bind<LoadingScreenProvider>()
                .AsSingle()
                .NonLazy();
        }
    }
}
