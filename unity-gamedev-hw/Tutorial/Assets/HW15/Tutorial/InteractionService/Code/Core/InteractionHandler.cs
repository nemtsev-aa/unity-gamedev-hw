using System;
using Tutorial;
using UnityEditor;
using UnityEngine;
using BehaviorTree.PlayerCoreSubsystem;
using InputService;

namespace InteractionService {

    public enum InteractionSearchMethods {
        Raycast,
        OverlapSphere
    }

    [Serializable]
    public sealed class InteractionHandler {

        public event Action<InteractionSource> InteractionStarted;
        public event Action<InteractionSource> InteractionComplited;

        public bool IsActive { get; private set; } = false;
        public bool HasInteraction {
            get {

                if (_currentInteraction == null)
                    return false;

                float distance = Vector3.Distance(_currentInteraction.transform.position, _root.transform.position);

                return distance <= DistanceToInteraction;
            }
        }

        [SerializeField] private InteractionSearchMethods _searchMethod;

        private Transform _root;
        private PlayerConfig _config;
        private InputController _inputController;
        private InteractionSource _currentInteraction;
        private Vector3 _currentMousePosition;

        private LayerMask _layerMask => _config.InteractionLayerMask;
        private float DistanceToInteraction => _config.InteractionDistance;

        public void Init(InputController inputController,
                         Transform root,
                         PlayerConfig config) {

            _root = root;
            _config = config;

            _inputController = inputController;
            _inputController.MousePositionChanged += InputController_MousePositionChanged;
            _inputController.ActionKeyClicked += InputController_ActionKeyClicked;

            IsActive = true;
        }

        public void FixedUpdate() {
            if (IsActive == false)
                return;

            if (_searchMethod == InteractionSearchMethods.Raycast)
                GetInteraction_RaycastFromMousePosition();
            else if (_searchMethod == InteractionSearchMethods.OverlapSphere)
                GetInteraction_OverlapSphere();
        }

        private void InputController_MousePositionChanged(Vector3 mousePosition) {

            if (_currentMousePosition != mousePosition)
                _currentMousePosition = mousePosition;
        }

        private void InputController_ActionKeyClicked() {

            if (_currentInteraction != null)
                OnActionClicked();
        }

        private void GetInteraction_RaycastFromMousePosition() {
            Ray ray = Camera.main.ScreenPointToRay(_currentMousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) == true) {
                Debug.DrawRay(Camera.main.transform.position, ray.direction * 500, Color.red);

                if (hit.collider.TryGetComponent(out InteractionSource interaction) == true) {
                    float distance = Vector3.Distance(interaction.transform.position, _root.transform.position);

                    if (distance <= DistanceToInteraction) {
                        _currentInteraction = interaction;
                        return;
                    }
                }

                _currentInteraction = null;
            }
        }

        private void GetInteraction_OverlapSphere() {
            Collider[] colliders = Physics.OverlapSphere(_root.transform.position,
                                                         DistanceToInteraction,
                                                         _layerMask,
                                                         QueryTriggerInteraction.Ignore);

            if (colliders.Length == 0) {
                _currentInteraction = null;

                return;
            }

            for (int i = 0; i < colliders.Length; i++) {

                if (colliders[i].TryGetComponent(out InteractionSource interaction) == true)
                    _currentInteraction = interaction;
            }
        }

        private void OnActionClicked() {

            _currentInteraction.Activate();
            _currentInteraction.PercentageCompletionChanged += OnPercentageCompletionChanged;

            InteractionStarted?.Invoke(_currentInteraction);
        }

        private void OnPercentageCompletionChanged(float percent) {

            if (percent == 1)
                InteractionComplited?.Invoke(_currentInteraction);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected() {
            Handles.color = Color.yellow;
            Handles.DrawWireDisc(_root.transform.position, Vector3.up, DistanceToInteraction);
        }
#endif
    }
}