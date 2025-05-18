using Atomic.Elements;
using Atomic.Entities;
using AtomicFramework.Conditions;
using AtomicFramework.MoveCompanent;
using System;
using UnityEngine;

namespace AtomicFramework.RotationCompanent {

    public sealed class RotateToTargetBehaviour : IEntityInit, IEntityUpdate {
        private IEntity _entity;
        private Transform _root;
        private ReactiveVariable<Transform> _targetVariable;
        private IValue<bool> _canRotate;

        private Transform _target;
        private float _rotateSpeed;

        public void Init(IEntity entity) {
            _entity = entity;
            _root = entity.GetRoot();

            _targetVariable = entity.GetTargetTransform();
            _targetVariable.Subscribe(OnTargetVariableChanged);

            if (_targetVariable.Value != null)
                OnTargetVariableChanged(_targetVariable.Value);
        }

        public void OnUpdate(IEntity entity, float deltaTime) {

            if (_canRotate == null)
                return;

            if (_canRotate.Value == false)
                return;

            if (TryGetRotate(out float targetAngle) == true)
                ApplyRotation(targetAngle, deltaTime);
        }

        private void OnTargetVariableChanged(Transform transform) {
            _target = transform;
            _canRotate = CreateRotateToTargetCondition(_entity);

            //Debug.Log($"RotateToTargetBehaviour: OnTargetVariableChanged {transform.gameObject.name}");
        }

        private IValue<bool> CreateRotateToTargetCondition(IEntity entity) {
            var isDead = entity.GetIsDeath();
            _rotateSpeed = entity.GetRotateSpeed();
            var isMoving = entity.GetIsMoving();
            _target = entity.GetTargetTransform().Value;

            if (_target.TryGetComponent(out SceneEntity targetEntity) == false)
                throw new ArgumentException($"SceneEntity is empty!");

            var targetIsDead = targetEntity.GetIsDeath();
            var rotateCondition = new RotateCondition(isDead, _rotateSpeed);
            var rotateToTargetCondition = new MoveToTargetCondition(rotateCondition, targetIsDead);

            return rotateToTargetCondition;
        }

        private bool TryGetRotate(out float targetAngle) {
            Vector3 direction = _target.position - _root.position;
            direction.y = 0;

            if (direction != Vector3.zero) {
                targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                return true;
            }

            targetAngle = 0f;
            return false;
        }

        private void ApplyRotation(float targetAngle, float deltaTime) {
            Quaternion slerpTarget = Quaternion.Euler(0, targetAngle, 0);
            _root.rotation = Quaternion.Slerp(
                _root.rotation,
                slerpTarget,
                _rotateSpeed * deltaTime
            );
        }
    }
}


