using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace InputService {

    public sealed class InputHandler : IDisposable {
        public event Action<Vector3> TargetPositionChanged;

        private readonly InputController _controller;
        private bool _isActive = true;

        private InputHandler(InputController inputController) {
            _controller = inputController;
            _controller.LeftMouseButtonClicked += OnLeftMouseButtonClicked;
        }

        public void Activate(bool status) {
            _isActive = status;
        }

        private void OnLeftMouseButtonClicked(Vector3 position) {
            if (_isActive == false)
                return;

            if (EventSystem.current.IsPointerOverGameObject() == false)
                ClickToGetNavMeshPoint(position);
        }

        private void ClickToGetNavMeshPoint(Vector3 position) {
            Ray ray = Camera.main.ScreenPointToRay(position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity) == true) {
                Vector3 hitPoint = hit.point;

                if (IsPointOnNavMesh(hitPoint) == true)
                    TargetPositionChanged?.Invoke(hitPoint);
            }
        }

        private bool IsPointOnNavMesh(Vector3 point) {
            NavMeshHit navMeshHit;

            if (NavMesh.SamplePosition(point, out navMeshHit, 0.1f, NavMesh.AllAreas) == true)
                return true;

            return false;
        }

        public void Dispose() {
            _controller.LeftMouseButtonClicked -= OnLeftMouseButtonClicked;
        }
    }
}

