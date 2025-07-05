using Zenject;
using UnityEngine;

namespace CharacterInfoWidget {
    public sealed class CharacterInfoInstaller : MonoInstaller {

        public override void InstallBindings() {

            Container.Bind<CharacterInfo>()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<CharacterInfoViewModel>()
                .AsSingle()
                .NonLazy();
        }
    }
}