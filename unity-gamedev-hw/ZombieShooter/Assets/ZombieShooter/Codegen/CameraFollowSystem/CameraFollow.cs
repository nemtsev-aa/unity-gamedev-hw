using Atomic.Contexts;
using AtomicFramework.Contextes;
using UnityEngine;
using ZombieShooter.GameCycleSystem;

namespace AtomicFramework.CameraFollowSystem {

    public class CameraFollow : IContextInit,
                                IContextLateUpdate,
                                IContextFixedUpdate,
                                IGameStartListener,
                                IGameFinishListener {

        private IContext _context;
        private Camera _camera;
        private Transform _target;
        private Vector3 _currentVelocity;

        private CameraFollowConfig _config;
        private Vector3 Offset => _config.Offset;
        private float SmoothSpeed => _config.SmoothSpeed;
        private bool LookAtTarget => _config.LookAtTarget;
        private float RotationSmoothness => _config.RotationSmoothness;
        private bool UseFixedUpdate => _config.UseFixedUpdate;
        private float DistanceThreshold => _config.DistanceThreshold;

        public void Init(IContext context) {
            _context = context;
            _camera = Camera.main;
            _config = context.GetCameraConfig();
        }

        public void OnStartGame() {
            _target = _context.GetCharacter().transform;
        }

        public void OnFinishGame() {
            _target = _context.GetContainersPresenter().WorldContainer;
        }

        public void LateUpdate(IContext context, float deltaTime) {

            if (UseFixedUpdate == false)
                FollowTarget(Time.deltaTime);
        }

        public void FixedUpdate(IContext context, float deltaTime) {

            if (UseFixedUpdate == true)
                FollowTarget(Time.fixedDeltaTime);
        }

        private void FollowTarget(float deltaTime) {

            if (_target == null)
                return;

            Vector3 desiredPosition = _target.position + Offset;

            _camera.transform.position = Vector3.SmoothDamp(
                _camera.transform.position,
                desiredPosition,
                ref _currentVelocity,
                SmoothSpeed * deltaTime,
                Mathf.Infinity,
                deltaTime
            );

            if (LookAtTarget == true) {
                Quaternion targetRotation = Quaternion.LookRotation(_target.position - _camera.transform.position);
                _camera.transform.rotation = Quaternion.Slerp(
                     _camera.transform.rotation,
                    targetRotation,
                    RotationSmoothness * deltaTime
                );
            }

            if (Vector3.Distance(_camera.transform.position, desiredPosition) < DistanceThreshold == true) {
                _camera.transform.position = desiredPosition;
            }
        }
    }
}
