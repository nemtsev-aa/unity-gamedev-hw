using UnityEngine;
using Atomic.Elements;
using Atomic.Entities;
using AtomicFramework.Conditions;

namespace AtomicFramework.MoveCompanent {

    public sealed class OneDirectionMoveBehaviour : IEntityInit, IEntityUpdate {
        private Transform _root;
        private ReactiveVariable<Vector3> _moveDirection;

        private ReactiveVariable<float> _speed;
        private ReactiveVariable<bool> _isMoving;

        private IValue<bool> _canMove;

        public void Init(IEntity entity) {
            _root = entity.GetRoot();
            _moveDirection = entity.GetMoveDirection();

            _isMoving = entity.GetIsMoving();
            _speed = entity.GetMoveSpeed();

            _canMove = CreateMoveCondition(entity);
        }

        public void OnUpdate(IEntity entity, float deltaTime) {
            _moveDirection = entity.GetMoveDirection();
            _isMoving.Value = _moveDirection.Value.sqrMagnitude > 0;

            //Debug.Log($"OneDirectionMoveBehaviour: OnUpdate {_entity.Name} {_moveDirection.Value}");

            if (_canMove.Value == false)
                return;

            _root.localPosition += _root.transform.forward * (_speed.Value * deltaTime);
        }

        private IValue<bool> CreateMoveCondition(IEntity entity) {
            var isDeath = entity.GetIsDeath();
            
            return new MoveCondition(isDeath, _speed, _isMoving);
        }
    }
}
