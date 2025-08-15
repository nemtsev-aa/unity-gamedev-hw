using UnityEngine;

namespace CharactersSystem {
    public interface ICharacter {
        void Move(Vector3 ditection, float deltaTime);
    }
}