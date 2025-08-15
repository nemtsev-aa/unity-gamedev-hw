using CharactersSystem.Player;
using CharactersSystem.Spawner;
using UnityEngine;
using Zenject;

namespace SampleGame.Gameplay {

    public sealed class CharacterSpawnerInstaller : MonoInstaller {
        [Space, SerializeField] private CharacterPrefabReferenceProvider _characterPrefabRefProvider;
        [Space, SerializeField] private PlayerCharacterConfig _playerConfig;

        public override void InstallBindings() {
            BindCharacterSpawnerCompanents();
        }

        private void BindCharacterSpawnerCompanents() {
            Container.BindInstance(_characterPrefabRefProvider)
                     .AsSingle()
                     .NonLazy();

            Container.BindInstance(_playerConfig)
                     .AsSingle()
                     .NonLazy();

            Container.Bind<CharacterFactory>()
                     .AsSingle();

            Container.Bind<CharacterSpawner>()
                     .AsSingle()
                     .NonLazy();
        }
    }
}