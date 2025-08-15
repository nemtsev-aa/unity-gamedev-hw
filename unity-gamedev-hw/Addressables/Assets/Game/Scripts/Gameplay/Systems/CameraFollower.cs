using CharactersSystem.Player;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Zenject;

namespace SampleGame
{
    public sealed class CameraFollower : ILateTickable
    {
        private readonly Camera _camera;
        private readonly Vector3 _cameraOffset;
        private IPlayerCharacter _character;

        public CameraFollower(Camera camera, Vector3 cameraOffset) {
            _camera = camera;
            _cameraOffset = cameraOffset;
        }

        public void SetTarget(IPlayerCharacter character) {
            _character = character;
        }

        void ILateTickable.LateTick() {

            if (_character == null)
                return;

            var cameraPosition = _character.GetPosition() + _cameraOffset;
            _camera.transform.position = cameraPosition;
        }
    }
}
