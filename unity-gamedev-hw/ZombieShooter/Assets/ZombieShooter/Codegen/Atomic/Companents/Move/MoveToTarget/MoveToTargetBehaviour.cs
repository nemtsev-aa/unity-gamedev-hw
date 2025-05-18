using System;
using UnityEngine;
using Atomic.Elements;
using Atomic.Entities;
using AtomicFramework.Conditions;

namespace AtomicFramework.MoveCompanent {

    public sealed class MoveToTargetBehaviour : IEntityInit, IEntityLateUpdate {
        private IEntity _entity;
        private Transform _root;
        private Transform _target;

        private ReactiveVariable<float> _speed;
        private ReactiveVariable<bool> _isMoving;
        private ReactiveVariable<Vector3> _moveDirection;

        private float _stoppingDistance;
        private ReactiveVariable<bool> _isWithinReach;
        private ReactiveVariable<Transform> _targetVariable;
        private IValue<bool> _canMove;

        public void Init(IEntity entity) {
            _entity = entity;

            _root = entity.GetRoot();
            _moveDirection = entity.GetMoveDirection();
            _stoppingDistance = entity.GetRange();
            _isWithinReach = entity.GetIsWithinReach();

            _targetVariable = entity.GetTargetTransform();
            _targetVariable.Subscribe(OnTargetVariableChanged);

            if (_targetVariable.Value != null)
                OnTargetVariableChanged(_targetVariable.Value);
        }

        public void OnLateUpdate(IEntity entity, float deltaTime) {
            if (_canMove == null)
                return;

            CheckeMoveStatus();

            if (_canMove.Value == false)
                return;

            _root.position += _moveDirection.Value.normalized * (_speed.Value * deltaTime);
        }

        private void CheckeMoveStatus() {
            Vector3 currentDirection = _target.position - _root.position;
            float distanceToTarget = currentDirection.sqrMagnitude;
            _isWithinReach.Value = distanceToTarget <= _stoppingDistance;

            if (_isWithinReach.Value == true) {
                _isMoving.Value = !_isWithinReach.Value;
                _moveDirection.Value = Vector3.zero;

                return;
            }

            _isMoving.Value = !_isWithinReach.Value;
            _moveDirection.Value = currentDirection;
        }

        private void OnTargetVariableChanged(Transform transform) {
            _target = transform;
            _canMove = CreateMoveToTargetCondition(_entity);
            _entity.SetCanMove(_canMove);

            //Debug.Log($"MoveToTargetBehaviour: OnTargetVariableChanged {transform.gameObject.name}");
        }

        private IValue<bool> CreateMoveToTargetCondition(IEntity entity) {
            var isDead = entity.GetIsDeath();
            _speed = entity.GetMoveSpeed();
            _isMoving = entity.GetIsMoving();
 
            if (_target.TryGetComponent(out SceneEntity targetEntity) == false)
                throw new ArgumentException($"SceneEntity is empty!");

            var moveCondition = new MoveCondition(isDead, _speed, _isMoving);
            var targetIsDead = targetEntity.GetIsDeath();
            var moveToTargetCondition = new MoveToTargetCondition(moveCondition, targetIsDead);

            return moveToTargetCondition;
        }
    }
}
