using Zenject;
using UnityEngine;

namespace Character {

    public sealed class CharacterModelInstaller : MonoInstaller {
        [SerializeField] private CharacterDefaultData _defaultData;

        public override void InstallBindings() {

            Container.Bind<CharacterModel>()
                .AsSingle()
                .WithArguments(_defaultData)
                .NonLazy();

            Container.Bind<CharacterStatesProvider>()
                .AsSingle()
                .NonLazy();
        }
    }
}
