using System;
using UnityEngine;

namespace AtomicFramework.MoveCompanent {

    [Serializable]
    public class MoveComponent {

        [SerializeField] private Transform _root;
        [SerializeField] private float _speed = 3f;

        private Vector3 _moveDirection;

        public MoveComponent(Transform root, float speed) {
            _root = root;
            _speed = speed;
        }

        public void SetDirection(Vector3 moveDirection) {
            _moveDirection = moveDirection;
        }

        public void Move() {
            _root.position += _moveDirection * (_speed * Time.deltaTime);
        }
    }
}
