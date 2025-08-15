using SampleGame.Core;
using System;
using Zenject;

namespace GameCycleSystem {

    [Serializable]
    public sealed class GameCycleInitializerInstaller : MonoInstaller {

        public override void InstallBindings() {

            Container.Bind<GameCycleInitializer>()
                     .AsSingle()
                     .NonLazy();
        }
    }
}



