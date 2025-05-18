using System;
using UnityEngine;
using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;
using ZombieShooter.GameCycleSystem;

namespace AtomicFramework.InputSystem {

    [Serializable]
    public class CharacterInputHandler : IContextInit,
                                         IContextDisable,
                                         IGameStartListener,
                                         IGamePauseListener,
                                         IGameFinishListener {

        private SceneEntity _characterEntity;
        private InputController _inputController;

        private ReactiveVariable<Vector3> _moveDirection;
        private IContext _context;
        private ReactiveVariable<Vector3> _pointerPosition;
        private IEvent _characterAttackRequest;
        private IValue<bool> _canAttack;

        public bool IsActive { get; private set; }
        public bool IsPause { get; private set; }

        public void Init(IContext context) {
            _context = context;
 
            _inputController = context.GetSystem<InputController>();
            _inputController.MoveDirection.Subscribe(OnMoveDirectionChanged);
            _inputController.IsShoot.OnEvent += OnShoot;

            _inputController.PointerPosition.Subscribe(OnPointerPositionChanged);
        }

        public void OnStartGame() {
            IsActive = true;

            _characterEntity = _context.GetCharacter();
            _moveDirection = _characterEntity.GetMoveDirection();
            _pointerPosition = _characterEntity.GetPointerPosition();

            _canAttack = _characterEntity.GetCanAttack();
            _characterAttackRequest = _characterEntity.GetAttackRequest();
        }

        public void OnPauseGame() {
            IsPause = !IsPause;
        }

        public void OnFinishGame() {
            IsActive = false;

            _characterEntity = null;
            _moveDirection = Vector3.zero;

            _canAttack = null;
            _characterAttackRequest = null;
        }

        private void OnMoveDirectionChanged(Vector3 direction) {

            if (IsActive == false || IsPause == true)
                return;

            _moveDirection.Value = direction;
        }

        private void OnShoot() {

            if (IsActive == false || IsPause == true)
                return;

            if (_canAttack.Value == true)
                _characterAttackRequest.Invoke();
        }

        private void OnPointerPositionChanged(Vector3 position) {

            if (IsActive == false || IsPause == true)
                return;

            _pointerPosition.Value = position;
        }

        public void Disable(IContext context) {
            _inputController.MoveDirection.Unsubscribe(OnMoveDirectionChanged);
            _inputController.IsShoot.OnEvent -= OnShoot;
            _inputController.PointerPosition.Unsubscribe(OnPointerPositionChanged);

        }
    }
}

