using InputService;
using R3;
using System;
using Tutorial;
using UnityEngine;

namespace CursorChangeService {

    [Serializable]
    public sealed class CursorChangeRaycaster : IDisposable {
        public ReadOnlyReactiveProperty<CursorType> CurrentCursorType => _currentCursorType;

        private float _raycastDistance => _viewModel.RaycastDistance;
        private LayerMask _interactableLayers => _viewModel.InteractableLayers;

        private readonly InputController _inputController;
        private ICursorChangeRaycasterViewModel _viewModel;
        private ReactiveProperty<CursorType> _currentCursorType;
        private Vector3 _currentMousePosition;

        public CursorChangeRaycaster(InputController inputController,
                                     CursorChangeRaycasterViewModel viewModel) {

            _inputController = inputController;
            _viewModel = viewModel;
            _currentCursorType = new ReactiveProperty<CursorType>();

            _inputController.MousePositionChanged += InputController_MousePositionChanged;
        }

        public void Activate() {
            Ray ray = Camera.main.ScreenPointToRay(_currentMousePosition);
            RaycastHit[] hitsArray = Physics.RaycastAll(ray, _raycastDistance, _interactableLayers);

            foreach (var iHit in hitsArray) {

                if (iHit.collider.TryGetComponent(out ICursorChanger cursorChanger) == false)
                    continue;

                var cursorType = cursorChanger.GetCursorType();

                //Debug.Log($"{cursorType}");

                if (_currentCursorType.Value != cursorType)
                    _currentCursorType.Value = cursorType;

                return;
            }

            if (_currentCursorType.Value != CursorType.Default)
                _currentCursorType.Value = CursorType.Default;

        }

        private void InputController_MousePositionChanged(Vector3 position) {

            if (_currentMousePosition != position)
                _currentMousePosition = position;
        }

        public void Dispose() {
            _inputController.MousePositionChanged -= InputController_MousePositionChanged;
        }
    }
}

