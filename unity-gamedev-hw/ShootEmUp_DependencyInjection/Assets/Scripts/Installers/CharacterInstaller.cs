using UnityEngine;
using Zenject;

public sealed class CharacterInstaller : MonoInstaller {
    [SerializeField] private CharacterConfig _characterConfig;
    [SerializeField] private Transform _characterSpawnPoint;
    [SerializeField] private Character _characterPrefab;

    public override void InstallBindings() {
        BindCharacter();
    }

    private void BindCharacter() {
        Container.BindInstance(_characterConfig).AsSingle();

        Character character = Container.InstantiatePrefabForComponent<Character>(_characterPrefab);
        character.transform.SetParent(_characterSpawnPoint);

        Container.Bind<Character>().FromInstance(character).AsSingle();
    }
}
