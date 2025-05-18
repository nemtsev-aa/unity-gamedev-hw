using Atomic.Elements;
using Atomic.Entities;
using System;
using UnityEngine;
using ZombieShooter.SceneObjects;

namespace AtomicFramework.CollisionMechanics {

    [Serializable]
    public sealed class CollisionInstaller : IEntityInstaller {
        [SerializeField] private float _radius;
        [SerializeField] private float _scanInterval;
        [SerializeField] private LayerMask _layerMask;

        private CollisionConfig _config;

        public void Install(IEntity entity) {
            entity.AddScanRadius(_radius);
            entity.AddScanInreval(_scanInterval);
            entity.AddScanerLayerMask(_layerMask);
            entity.AddClosestUnit(new ReactiveVariable<Unit>());

            var collision = new CollisionBehavior<Enemy>();

            entity.AddBehaviour(new EnemyEntityScanner(collision));
        }

        public void SetConfig(CollisionConfig config) {
            _config = config;

            SetRadius(_config.Radius);
            SetScanInterval(_config.ScanInterval);
            SetLayerMask(_config.LayerMask);
        }

        private void SetRadius(float radius) {
            _radius = radius;
        }

        private void SetScanInterval(float scanInterval) {
            _scanInterval = scanInterval;
        }

        private void SetLayerMask(LayerMask layerMask) {
            _layerMask = layerMask;
        }
    }
}