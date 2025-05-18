using System;
using UnityEngine;
using Atomic.Contexts;
using Atomic.Elements;

namespace AtomicFramework.InputSystem {
    
    [Serializable]
    public sealed class InputController : IContextInit, IContextUpdate {

        private InputConfig _config;

        private KeyCode Forward => _config.Forward;
        private KeyCode Back => _config.Back;
        private KeyCode Left => _config.Left;
        private KeyCode Right => _config.Right;
        private int ShootButton => _config.MouseButton;

        public ReactiveVariable<Vector3> MoveDirection { get; private set; }
        public BaseEvent IsShoot { get; private set; }
        public BaseEvent<Vector3> PointerPosition { get; private set; }

        public void Init(IContext context) {
            _config = context.GetInputConfig();

            MoveDirection = new ReactiveVariable<Vector3>();
            IsShoot = new BaseEvent();
            PointerPosition = new BaseEvent<Vector3>();
        }

        public void Update(IContext context, float deltaTime) {
            HandleMouse();
            HandleKeyboard();
        }

        private void HandleMouse() {

            if (Input.GetMouseButtonDown(ShootButton) == true)
                IsShoot?.Invoke();

            PointerPosition?.Invoke(Input.mousePosition);
        }

        private void HandleKeyboard() {

            if (Input.GetKey(Forward))
                SetDirection(Vector3.forward);
            else if (Input.GetKey(Back))
                SetDirection(Vector3.back);
            else if (Input.GetKey(Left))
                SetDirection(Vector3.left);
            else if (Input.GetKey(Right))
                SetDirection(Vector3.right);
            else
                SetDirection(Vector3.zero);
        }

        private void SetDirection(Vector3 direction) {
            MoveDirection.Value = direction;
        }
    }
}

