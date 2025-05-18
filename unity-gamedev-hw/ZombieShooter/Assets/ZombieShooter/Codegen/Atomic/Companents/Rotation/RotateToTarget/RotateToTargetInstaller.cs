using System;
using UnityEngine;
using Atomic.Entities;

namespace AtomicFramework.RotationCompanent {

    [Serializable]
    public class RotateToTargetInstaller : IEntityInstaller {
        [SerializeField] private float _rotateSpeed;

        public void Install(IEntity entity) {
            entity.AddRotateSpeed(_rotateSpeed);

            entity.AddBehaviour(new RotateToTargetBehaviour());
        }

        public void SetConfig(RotateConfig config) {
            _rotateSpeed = config.RotateSpeed;
        }
    }
}


