using UnityEngine;


namespace CharactersSystem.Player.Components {

    public sealed class RotationComponent : MonoBehaviour {
        [SerializeField] private Transform _root;
        [SerializeField] private float _rotationSpeed = 10f;
        [SerializeField] private float _minMovementThreshold = 0.1f;

        private Quaternion _targetRotation;

        public void SetDirection(Vector3 moveDirection) {

            if (moveDirection.magnitude > _minMovementThreshold)
                RotateTowardsMovement(moveDirection);
        }

        private void RotateTowardsMovement(Vector3 moveDirection) {
            moveDirection.y = 0;

            _targetRotation = Quaternion.LookRotation(moveDirection);

            _root.rotation = Quaternion.Slerp(
                _root.rotation,
                _targetRotation,
                _rotationSpeed * Time.deltaTime
            );
        }
    }
}
