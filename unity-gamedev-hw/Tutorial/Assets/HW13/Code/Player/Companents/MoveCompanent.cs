using System;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree.PlayerCoreSubsystem;
using static UnityEngine.Rendering.STP;

namespace BehaviorTree.PlayerCompanents {

    public enum MoveStatus {
        PathPending = 0,
        HasPath = 1,
        PathComplited = 2,
        None = 3
    }

    [Serializable]
    public sealed class MoveCompanent : IPlayerCompanent {
        public Transform Root { get; private set; }
        public bool IsActive { get; private set; }
        public float Velocity => _agent.velocity.magnitude;

        [SerializeField] private NavMeshAgent _agent;
        [Space, SerializeField] private GameObject _pointer;
        [SerializeField] private bool _pointerIsShow = false;

        private float _defaultPointerPositionY;
        private Vector3 _currentTarget;

        public void Init(Transform root, PlayerConfig config) {
            Root = root;
            IsActive = true;
            _agent.speed = config.MoveSteed;

            _pointer.transform.SetParent(null);
            _defaultPointerPositionY = _pointer.transform.position.y;
        }

        public void Activate(bool status) {
            IsActive = status;
        }

        public void SetMoveSpeed(float value) {
            _agent.speed = value;
        }

        public void SetStoppingDistance(float value) {

            if (value > 0)
                _agent.stoppingDistance = value;
        }

        public void MoveToTarget(Vector3 position) {
            _agent.isStopped = false;

            _currentTarget = position;

            if (position == null)
                return;

            if (position != Vector3.zero)
                _agent.SetDestination(position);

            if (_pointerIsShow == true) {
                _pointer.transform.position = new Vector3(position.x,
                                                         _defaultPointerPositionY,
                                                         position.z);
                ShowPointer();
            }
        }

        public MoveStatus GetMoveStatus(out float distanceToTarget) {

            if (_agent.pathPending == true) {
                distanceToTarget = GetDistanceToTarget();
                return MoveStatus.PathPending;
            }

            if (_agent.hasPath == true) {

                if (_agent.remainingDistance <= _agent.stoppingDistance) {
                    distanceToTarget = _agent.remainingDistance;
                    return MoveStatus.PathComplited;
                }

                distanceToTarget = _agent.remainingDistance;
                return MoveStatus.HasPath;
            }

            distanceToTarget = GetDistanceToTarget();
            return MoveStatus.None;
        }

        public float GetDistanceToTarget() {

            if (_currentTarget == null)
                return 0;

            return (_currentTarget - Root.position).sqrMagnitude - _agent.stoppingDistance;
        }

        private void ShowPointer() {
            _pointer.gameObject.SetActive(_pointerIsShow);
        }
    }
}

