using System;
using UnityEngine;

namespace ShootEmUp {
    public sealed class LevelBackground : MonoBehaviour, IGameStartListener,
                                          IGameFinishListener, IGamePauseListener,
                                          IGameFixedUpdateListener {
        
        [SerializeField] private Params _params;
        
        private float _startPositionY;
        private float _endPositionY;
        private float _movingSpeedY;
        private float _positionX;
        private float _positionZ;

        public bool IsActive { get; private set; }
        public bool IsPause { get; private set; }

        public void OnStartGame() {
            _startPositionY = _params.StartPositionY;
            _endPositionY = _params.EndPositionY;
            _movingSpeedY = _params.MovingSpeedY;

            var position = transform.position;
            _positionX = position.x;
            _positionZ = position.z;

            IsActive = true;
        }

        public void OnPauseGame() {
            IsPause = !IsPause;
        }

        public void OnFinishGame() {
            IsActive = false;
        }

        public void OnFixedUpdateGame() {
            if (IsActive == false || IsPause == true)
                return;

            if (transform.position.y <= _endPositionY) {
                transform.position = new Vector3(
                    _positionX,
                    _startPositionY,
                    _positionZ
                );
            }

            transform.position -= new Vector3(
                _positionX,
                _movingSpeedY * Time.fixedDeltaTime,
                _positionZ
            );
        }

        [Serializable]
        public sealed class Params {
            [field: SerializeField] public float StartPositionY { get; private set; }
            [field: SerializeField] public float EndPositionY { get; private set; }
            [field: SerializeField] public float MovingSpeedY { get; private set; }
        }
    }
}