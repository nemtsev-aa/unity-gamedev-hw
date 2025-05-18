using System;
using UnityEngine;
using Atomic.Elements;
using Atomic.Entities;
using AtomicFramework.MoveCompanent;

namespace AtomicFramework.ZombieShooter {

    [Serializable]
    public class MoveInstaller : IEntityInstaller {
        private float _moveSpeed;
        private Vector3 _moveDirection;
        private bool _isMoving = false;

        private MoveConfig _config;

        public void Install(IEntity entity) {
            entity.AddMoveSpeed(_moveSpeed);
            entity.AddMoveDirection(_moveDirection);
            entity.AddIsMoving(new ReactiveVariable<bool>(_isMoving));

            entity.AddBehaviour(new MoveBehaviour());
        }

        public void SetConfig(MoveConfig config) {
            _config = config;

            SetMoveSpeed(_config.MoveSpeed);
            SetIsMoving(_config.IsMoving);
            SetMoveDirection(_config.MoveDirection);
        }

        private void SetMoveSpeed(float moveSpeed) {
            _moveSpeed = moveSpeed;
        }

        private void SetMoveDirection(Vector3 moveDirection) {
            _moveDirection = moveDirection;
        }

        private void SetIsMoving(bool status) {
            _isMoving = status;
        }
    }
}


