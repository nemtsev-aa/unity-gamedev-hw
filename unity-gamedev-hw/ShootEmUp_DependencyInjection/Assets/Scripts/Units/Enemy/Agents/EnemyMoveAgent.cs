using UnityEngine;

namespace ShootEmUp {
    public sealed class EnemyMoveAgent {

        private MoveController _moveController;
        private float _reachedOffset;

        private Vector2 _destination;
        private bool _isReached;

        public EnemyMoveAgent(MoveController moveController, float reachedOffset) {
            _moveController = moveController;
            _reachedOffset = reachedOffset;
        }

        public bool IsReached => _isReached;

        public void SetDestination(Vector2 endPoint) {
            _destination = endPoint;
            _isReached = false;
        }

        public void Move(Transform transform) {
            if (_isReached == true)
                return;

            var vector = _destination - (Vector2)transform.position;

            if (vector.magnitude <= _reachedOffset) {
                _isReached = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            _moveController.MoveByRigidbodyVelocity(direction);
        }
    }
}