using UnityEngine;

namespace ShootEmUp {

    public sealed class MoveController {
        private float _speed;
        private Rigidbody2D _rigidbody2D;
        private LevelBounds _levelBounds;

        public MoveController(Rigidbody2D rigidbody2D, float speed) {
            _rigidbody2D = rigidbody2D;
            _speed = speed;
        }

        public void SetLevelBounds(LevelBounds levelBounds) {
            _levelBounds = levelBounds;
        }

        public void MoveByRigidbodyVelocity(Vector2 vector) {
            Vector2 nextPosition = _rigidbody2D.position + vector * _speed;

            if (_levelBounds != null && CheckObjectPosition(nextPosition) == false)
                return;

            nextPosition = _rigidbody2D.position + vector * _speed;
            _rigidbody2D.MovePosition(nextPosition);
        }

        private bool CheckObjectPosition(Vector2 nextPosition) {
            return _levelBounds.InBounds(new Vector3(nextPosition.x, nextPosition.y, 0f));
        }
    }
}

