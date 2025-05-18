using UnityEngine;
using Atomic.Elements;
using Atomic.Entities;
using ZombieShooter.SceneObjects;

namespace AtomicFramework.CollisionMechanics {

    public sealed class CollisionBehavior<T> :
                        IEntityInit,
                        IEntityUpdate,
                        IDetectionBehavior where T : Unit {

        private Transform _root;
        private float _radius;
        private float _scanInterval;
        private LayerMask _layerMask;

        private float _scanTime;
        private ReactiveVariable<bool> _isScanning = new ReactiveVariable<bool>(false);
        private Collider[] _hitColliders = new Collider[20];

        public void Init(IEntity entity) {
            _root = entity.GetRoot();
            _radius = entity.GetScanRadius();
            _scanInterval = entity.GetScanInreval();
            _layerMask = entity.GetScanerLayerMask();

            entity.AddCanScane(_isScanning);
        }

        public void OnUpdate(IEntity entity, float deltaTime) {

            //Debug.Log($"CollisionBehavior: OnUpdate {_isScanning.Value}");

            if (_isScanning.Value == false) {
                _scanTime += deltaTime;

                if (_scanTime >= _scanInterval) {
                    _scanTime = 0;
                    _isScanning.Value = true;
                }

                return;
            }
        }

        public Unit FindClosestUnit() {
            _isScanning.Value = false;

            return PerformScan();
        }

        private T PerformScan() {
            Vector3 rootPosition = _root.transform.position;

            int numHits = Physics.OverlapSphereNonAlloc(
                rootPosition,
                _radius,
                _hitColliders,
                _layerMask,
                QueryTriggerInteraction.Collide
            );

            float closestDistance = Mathf.Infinity;
            T closestTarget = null;

            if (_hitColliders.Length == 1 && _hitColliders[0].gameObject != _root.gameObject) {
                Collider hit = _hitColliders[0];

                if (hit.TryGetComponent(out SceneEntityProxy proxy) == false)
                    return null;

                if (proxy.source.TryGetComponent(out T target) == false)
                    return null;

                return target;
            }

            for (int i = 0; i < numHits; i++) {
                Collider hit = _hitColliders[i];

                //Debug.Log($"_hitCollider {numHits}");

                if (hit == null)
                    continue;

                if (hit.gameObject == _root.gameObject)
                    continue;

                if (hit.TryGetComponent(out SceneEntityProxy proxy) == false)
                    continue;

                if (proxy.source.TryGetComponent(out T target) == false)
                    continue;

                float distance = Vector3.Distance(rootPosition, target.transform.position);

                if (distance < closestDistance) {
                    closestDistance = distance;
                    closestTarget = target;
                }
            }

            return closestTarget;
        }
    }
}
