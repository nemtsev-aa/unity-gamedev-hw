using System;
using Zenject;

namespace GameCycleSystem {

    [Serializable]
    public sealed class GameCycleInstaller : MonoInstaller {

        public override void InstallBindings() {

            Container.BindInterfacesAndSelfTo<GameCycle>()
                     .AsSingle();
        }
    }
}



