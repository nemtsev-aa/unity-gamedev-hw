using System;
using UnityEngine;

namespace ShootEmUp {
    public sealed class InputManager : MonoBehaviour, IGameStartListener,
                                       IGameFinishListener, IGamePauseListener,
                                       IGameUpdateListener {

        public const KeyCode LEFT_MOVE = KeyCode.LeftArrow;
        public const KeyCode RIGHT_MOVE = KeyCode.RightArrow;
        public const KeyCode FIRE = KeyCode.F;
        public const KeyCode PAUSE = KeyCode.P;

        public event Action<int> HorizontalDirectionChanged;
        public event Action FireStatusChanged;
        public event Action PauseStatusChanged;

        public bool IsActive { get; private set; }
        public bool IsPause { get; private set; }

        public void OnStartGame() {
            IsActive = true;
        }

        public void OnPauseGame() {
            IsPause = !IsPause;
        }

        public void OnFinishGame() {
            IsActive = false;
        }

        public void OnUpdateGame() {
            if (Input.GetKeyDown(PAUSE))
                PauseStatusChanged?.Invoke();

            if (IsActive == false || IsPause == true)
                return;

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