using CharactersSystem.Player;
using GameCycleSystem;
using UnityEngine;
using Zenject;

namespace SampleGame {

    public sealed class MoveController : IFixedTickable {
        private readonly IMoveInput _moveInput;
        private IPlayerCharacter _character;

        public MoveController(IMoveInput moveInput) {
            _moveInput = moveInput;
        }

        public void SetCharacter(IPlayerCharacter character) {
            _character = character;
        }

        void IFixedTickable.FixedTick() {

            if (_character == null)
                return;

            _character.Move(_moveInput.GetDirection().normalized, Time.deltaTime);
        }
    }
}