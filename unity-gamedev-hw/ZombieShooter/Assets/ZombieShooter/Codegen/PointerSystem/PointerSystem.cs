using UnityEngine;
using Atomic.Contexts;
using System.Collections.Generic;
using ZombieShooter.GameCycleSystem;
using Object = UnityEngine.Object;

namespace AtomicFramework.EnemyPointerSystem {

    public class PointerSystem : IContextInit,
                                 IContextLateUpdate,
                                 IGameStartListener,
                                 IGamePauseListener,
                                 IGameFinishListener {

        private IContext _context;
        private Camera _camera;
        private PointerIcon _pointerPrefab;
        private Transform _playerTransform;
        private Transform _pointerParent;

        private Dictionary<EnemyPointer, PointerIcon> _dictionary = new Dictionary<EnemyPointer, PointerIcon>();

        public bool IsActive { get; private set; }
        public bool IsPause { get; private set; }

        public void Init(IContext context) {
            _context = context;
            _camera = Camera.main;

            _pointerPrefab = context.GetPointerIconPrefab();
            _pointerParent = context.GetContainersPresenter().PointerIconContainer;
        }

        public void OnStartGame() {
            IsActive = true;
            _playerTransform = _context.GetCharacter().transform;
        }

        public void OnPauseGame() {
            IsPause = !IsPause;
        }

        public void OnFinishGame() {
            IsActive = false;

            ClearDictionary();
        }

        public void AddToList(EnemyPointer enemyPointer) {

            if (_dictionary.ContainsKey(enemyPointer) == false) {
                PointerIcon newPointer = Object.Instantiate(_pointerPrefab, _pointerParent);
                newPointer.Init();
                
                _dictionary.Add(enemyPointer, newPointer);
            }
        }

        public void RemoveFromList(EnemyPointer enemyPointer) {
            Object.Destroy(_dictionary[enemyPointer].gameObject);
            _dictionary.Remove(enemyPointer);
        }

        public void LateUpdate(IContext context, float deltaTime) {

            if (IsActive = false || IsPause == true)
                return;

            // Left, Right, Down, Up
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);

            foreach (var kvp in _dictionary) {

                EnemyPointer enemyPointer = kvp.Key;
                PointerIcon pointerIcon = kvp.Value;

                Vector3 toEnemy = enemyPointer.Enemy.transform.position - _playerTransform.position;
                Ray ray = new Ray(_playerTransform.position, toEnemy);

                float rayMinDistance = Mathf.Infinity;
                int index = 0;

                for (int planeIndex = 0; planeIndex < 4; planeIndex++) {

                    if (planes[planeIndex].Raycast(ray, out float distance)) {

                        if (distance < rayMinDistance) {
                            rayMinDistance = distance;
                            index = planeIndex;
                        }
                    }
                }

                rayMinDistance = Mathf.Clamp(rayMinDistance, 0, toEnemy.magnitude);
                Vector3 worldPosition = ray.GetPoint(rayMinDistance);
                Vector3 position = _camera.WorldToScreenPoint(worldPosition);
                Quaternion rotation = GetIconRotation(index);

                if (toEnemy.magnitude > rayMinDistance)
                    pointerIcon.Show();
                else
                    pointerIcon.Hide();

                pointerIcon.SetIconPosition(position, rotation);
                pointerIcon.SetDistanceToEnemy(toEnemy.magnitude);
            }
        }

        private Quaternion GetIconRotation(int planeIndex) {

            switch (planeIndex) {
                case 0:
                    return Quaternion.Euler(0f, 0f, 90f);

                case 1:
                    return Quaternion.Euler(0f, 0f, -90f);

                case 2:
                    return Quaternion.Euler(0f, 0f, 180);

                case 3:
                    return Quaternion.Euler(0f, 0f, 0f);

                default:
                    return Quaternion.identity;
            }
        }

        private void ClearDictionary() {
            foreach (var item in _dictionary.Keys) {
                Object.Destroy(_dictionary[item].gameObject);
            }

            _dictionary.Clear();
        }
    }
}
