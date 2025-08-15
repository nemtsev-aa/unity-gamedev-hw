using GameCycleSystem;
using UnityEngine;

namespace CharactersSystem.Player {

    public interface IPlayerCharacter : IGameUpdateListener, ICharacter {
        Vector3 GetPosition();
    }
}