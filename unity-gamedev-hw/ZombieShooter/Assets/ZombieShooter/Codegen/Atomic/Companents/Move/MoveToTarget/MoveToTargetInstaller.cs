using System;
using UnityEngine;
using Atomic.Elements;
using Atomic.Entities;

namespace AtomicFramework.MoveCompanent {

    [Serializable]
    public class MoveToTargetInstaller : IEntityInstaller {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private bool _isMoving;

        public void Install(IEntity entity) {
            entity.AddMoveSpeed(_moveSpeed);
            entity.AddMoveDirection(Vector3.zero);
            entity.AddIsMoving(new ReactiveVariable<bool>(_isMoving));
            entity.AddTargetTransform(new ReactiveVariable<Transform>());

            entity.AddBehaviour(new MoveToTargetBehaviour());
        }

        public void SetConfig(MoveConfig config) {
            SetMoveSpeed(config.MoveSpeed);
            SetIsMoving(config.IsMoving);
        }

        private void SetMoveSpeed(float moveSpeed) {
            _moveSpeed = moveSpeed;
        }

        private void SetIsMoving(bool isMoving) {
            _isMoving = isMoving;
        }
    }
}


