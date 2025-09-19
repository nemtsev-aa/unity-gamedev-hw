using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InputService {

    public sealed class InputController : ITickable {
        private const int MOUSE_BUTTON_INDEX = 0;
        private const KeyCode ACTION_KEY = KeyCode.E;

        public event Action<Vector3> LeftMouseButtonClicked;
        public event Action<Vector3> MousePositionChanged;
        public event Action ActionKeyClicked;

        public static bool IsInputBlocked => _inputBlockers.Count > 0;
        private static readonly List<object> _inputBlockers = new List<object>();
        private Vector3 _currentMousePosition;

        public static void BlockInput(object blocker) {
            
            if (_inputBlockers.Contains(blocker) == false)
                _inputBlockers.Add(blocker);
        }

        public static void UnblockInput(object blocker) {
            
            if (_inputBlockers.Contains(blocker) == true)
                _inputBlockers.Remove(blocker);
        }

        public void Tick() {

            if (IsInputBlocked == true)
                return;

            HandlePlayerInput();
        }

        private void HandlePlayerInput() {
            
            if (Input.GetMouseButtonDown(MOUSE_BUTTON_INDEX) == true) 
                LeftMouseButtonClicked?.Invoke(Input.mousePosition);

            if (Input.GetKeyDown(ACTION_KEY) == true)
                ActionKeyClicked?.Invoke();

            var mousePosition = Input.mousePosition;

            if (_currentMousePosition != mousePosition) {
                _currentMousePosition = mousePosition;
                MousePositionChanged?.Invoke(_currentMousePosition);
            }
        }
    }
}

