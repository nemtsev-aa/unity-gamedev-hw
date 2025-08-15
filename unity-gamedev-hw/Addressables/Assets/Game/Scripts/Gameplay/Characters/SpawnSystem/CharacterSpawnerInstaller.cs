using CharactersSystem.Player;
using UnityEngine;
using Zenject;

namespace CharactersSystem.Spawner {

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