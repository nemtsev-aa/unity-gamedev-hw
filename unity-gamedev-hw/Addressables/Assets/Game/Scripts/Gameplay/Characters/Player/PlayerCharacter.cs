using CharactersSystem.Player.Components;
using CharactersSystem.Player.Skins;
using GameCycleSystem;
using SampleGame;
using UnityEngine;

namespace CharactersSystem.Player {

    public sealed class PlayerCharacter : Character, IPlayerCharacter, IGamePauseListener {
        public int CurrentZoneIndex => _locationChecker.CurrentZoneIndex;
        public PlayerCharacterSkinManager SkinManager => _skinManager;

        [SerializeField] private LocationChecker _locationChecker;
        [SerializeField] private AnimationHandler _animationHandler;
        [SerializeField] private RotationComponent _rotationCompanent;
        [SerializeField] private PlayerCharacterSkinManager _skinManager;

        private float _speed;
        private bool _isPaused = false;

        public void Init(PlayerCharacterConfig config) {
            _speed = config.MoveSpeed;
            _animationHandler.Init();
            _skinManager.Init();

            _skinManager.SwitchSkin(config.DefaultSkinIndex);
        }

        public void Move(Vector3 direction, float deltaTime) {

            if (_isPaused == true)
                return;

            float moveVelocity = deltaTime * _speed;
            transform.position += direction * moveVelocity;

            _animationHandler.SetStateIndex(moveVelocity * direction.sqrMagnitude);
            _rotationCompanent.SetDirection(direction);
        }

        public Vector3 GetPosition() {
            return transform.position;
        }

        public void OnUpdateGame() {

            if (_isPaused == true)
                return;

            _locationChecker.Update();
        }

        public void OnPauseGame() {
            _isPaused = !_isPaused;
        }
    }
}