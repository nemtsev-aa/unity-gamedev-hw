using System;
using UnityEngine;

namespace ShootEmUp {
    public sealed class InputManager : MonoBehaviour {
        public const KeyCode LEFT_MOVE = KeyCode.LeftArrow;
        public const KeyCode RIGHT_MOVE = KeyCode.RightArrow;
        public const KeyCode FIRE = KeyCode.Space;

        public event Action<int> HorizontalDirectionChanged;
        public event Action FireStatusChanged;

        private void Update() {
            if (Input.GetKeyDown(FIRE))
                FireStatusChanged?.Invoke();

            if (Input.GetKey(LEFT_MOVE))
                HorizontalDirectionChanged?.Invoke(-1);
            else if (Input.GetKey(RIGHT_MOVE))
                HorizontalDirectionChanged?.Invoke(1);
            else
                HorizontalDirectionChanged?.Invoke(0);
        }
    }
}