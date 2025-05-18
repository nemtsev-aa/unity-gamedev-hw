using UnityEngine;
using Atomic.Elements;
using Atomic.Entities;
using AtomicFramework.Conditions;

namespace AtomicFramework.RotationCompanent {

    public sealed class MouseLookRotationBehaviour : IEntityInit, IEntityUpdate, IEntityEnable, IEntityDisable {
        private RotationModes _mode = RotationModes.Slerp;
        private Camera _mainCamera;
        private IEntity _entity;
        private Transform _root;
        private float _speed = 5f;
        private float _smoothTime = 0.1f;
        private ReactiveVariable<Vector3> _pointerPosition;
        private Plane _groundPlane;
        private float _currentVelocity;
        private IValue<bool> _canRotate;
        private bool _isActive;

        public void Init(IEntity entity) {
            _entity = entity;

            _root = _entity.GetRoot();
            _mode = _entity.GetRotationModes();
            _speed = _entity.GetRotateSpeed();
            _smoothTime = _entity.GetSmoothTime();
            _pointerPosition = _entity.GetPointerPosition();

            _mainCamera = Camera.main;
            _groundPlane = new Plane(Vector3.up, Vector3.zero);

            _entity.AddCanRotate(CreateRotateCondition(_entity));
        }

        public void Enable(IEntity entity) {
            _isActive = true;
        }

        public void Disable(IEntity entity) {
            _isActive = false;
        }

        public void OnUpdate(IEntity entity, float deltaTime) {

            if (_isActive == false)
                return;

            if (entity.GetCanRotate().Value == false)
                return;

            RotateTowardsMouse(deltaTime);
        }

        private void RotateTowardsMouse(float deltaTime) {
            Ray ray = _mainCamera.ScreenPointToRay(_entity.GetPointerPosition().Value);

            if (_groundPlane.Raycast(ray, out float enter)) {
                Vector3 mouseWorldPos = ray.GetPoint(enter);
                Vector3 direction = mouseWorldPos - _root.position;
                direction.y = 0;

                if (direction != Vector3.zero) {
                    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                    ApplyRotation(targetAngle, deltaTime);
                }
            }
        }

        private void ApplyRotation(float targetAngle, float deltaTime) {

            switch (_mode) {
                case RotationModes.Instant:
                    _root.rotation = Quaternion.Euler(0, targetAngle, 0);
                    break;

                case RotationModes.SmoothDamp:
                    float smoothAngle = Mathf.SmoothDampAngle(
                        _root.eulerAngles.y,
                        targetAngle,
                        ref _currentVelocity,
                        _smoothTime,
                        _speed
                    );
                    _root.rotation = Quaternion.Euler(0, smoothAngle, 0);
                    break;

                case RotationModes.Lerp:
                    Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
                    _root.rotation = Quaternion.Lerp(
                        _root.rotation,
                        targetRotation,
                        _speed * deltaTime
                    );
                    break;

                case RotationModes.Slerp:
                    Quaternion slerpTarget = Quaternion.Euler(0, targetAngle, 0);
                    _root.rotation = Quaternion.Slerp(
                        _root.rotation,
                        slerpTarget,
                        _speed * deltaTime
                    );
                    break;
            }
        }

        private IValue<bool> CreateRotateCondition(IEntity entity) {
            var isDead = entity.GetIsDeath();
            var isMoving = entity.GetIsMoving();
            var rotateCondition = new RotateCondition(isDead, _speed);

            return rotateCondition;
        }
    }
}
