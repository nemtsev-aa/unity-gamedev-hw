using UnityEngine;

namespace ZombieShooter.SceneObjects {
    public sealed class CharacterFactory {
        private readonly Character _prefab;

        public CharacterFactory(Character prefab) {
            _prefab = prefab;
        }

        public Character Get(Transform parent) {
            Character newCharacter = UnityEngine.Object.Instantiate(_prefab);
            newCharacter.transform.SetParent(parent);

            return newCharacter;
        }
    }
}
