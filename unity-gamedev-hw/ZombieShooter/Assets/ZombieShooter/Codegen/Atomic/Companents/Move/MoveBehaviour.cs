using UnityEngine;
using Atomic.Elements;
using Atomic.Entities;
using AtomicFramework.Conditions;

namespace AtomicFramework.ZombieShooter {

    public sealed class MoveBehaviour : IEntityInit, IEntityUpdate {
        private Transform _root;
        private ReactiveVariable<Vector3> _moveDirection;
        private ReactiveVariable<float> _speed;
        private ReactiveVariable<bool> _isMoving;

        public void Init(IEntity entity) {
            _root = entity.GetRoot();
            _moveDirection = entity.GetMoveDirection();
            _isMoving = entity.GetIsMoving();
            _speed = entity.GetMoveSpeed();

            entity.AddCanMove(CreateMoveCondition(entity));
        }

        public void OnUpdate(IEntity entity, float deltaTime) {

            _isMoving.Value = _moveDirection.Value.sqrMagnitude > 0;

            if (entity.GetCanMove().Value == false)
                return;

            Vector3 worldDirection = _root.TransformDirection(_moveDirection.Value.normalized);
            _root.localPosition += worldDirection * (entity.GetMoveSpeed().Value * deltaTime);
        }

        private IValue<bool> CreateMoveCondition(IEntity entity) {
            var isDead = entity.GetIsDeath();

            return new MoveCondition(isDead, _speed, _isMoving);
        }
    }
}
