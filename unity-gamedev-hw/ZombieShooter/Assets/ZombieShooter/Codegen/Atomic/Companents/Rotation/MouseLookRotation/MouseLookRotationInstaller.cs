using Atomic.Entities;
using System;
using UnityEngine;

namespace AtomicFramework.RotationCompanent {

    [Serializable]
    public sealed class MouseLookRotationInstaller : IEntityInstaller {
        [SerializeField] private RotationModes _rotationMode = RotationModes.Slerp;
        [SerializeField] private float _rotationSpeed = 5f;
        [SerializeField] private float _smoothTime = 0.1f;
        
        private RotateConfig _config;

        public void Install(IEntity entity) {
            entity.AddRotateSpeed(_rotationSpeed);
            entity.AddRotationModes(_rotationMode);
            entity.AddSmoothTime(_smoothTime);

            entity.AddBehaviour(new MouseLookRotationBehaviour());
        }

        public void SetConfig(RotateConfig config) {
            _config = config;

            SetRotationModes(_config.RotationMode);
            SetRotateSpeed(_config.RotateSpeed);
            SetSmoothTime(_config.SmoothTime);
        }

        private void SetRotateSpeed(float rotationSpeed) {
            _rotationSpeed = rotationSpeed;
        }

        private void SetRotationModes(RotationModes mode) {
            _rotationMode = mode;
        }

        private void SetSmoothTime(float smoothTime) {
            _smoothTime = smoothTime;
        }
    }
}
