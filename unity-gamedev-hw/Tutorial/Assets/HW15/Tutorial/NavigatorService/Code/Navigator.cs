using UnityEngine;

namespace NavigatorService {

    public sealed class Navigator : MonoBehaviour {
        public bool IsActive { get; private set; }

        [SerializeField] private TargetPointer _targetPointerPrefab;
        [SerializeField] private NavigationPointer _navigationPointerPrefab;
        [SerializeField] private float _deactivateDistance = 3f;

        private TargetPointer _targetPointer;
        private NavigationPointer _navigationPointer;
        private Transform _parent;

        public void Init(Transform parent) {
            _parent = parent;

            CreateNavigationPointer(_parent);
        }

        public void SetTarget(Transform target) {
            CreateTargetPointer(target);
            Activate(true);
        }

        public void Activate(bool status) {
            IsActive = status;

            _targetPointer.Activate(status);
            _navigationPointer.Activate(status);
        }

        public void ShowDirection() {

            if (IsActive == false)
                return;

            if (_navigationPointer == null || _targetPointer == null)
                return;

            float distanceToTarget = Vector3.Distance(_navigationPointer.transform.position,
                                                      _targetPointer.transform.position);

            Activate(distanceToTarget > _deactivateDistance);

            if (IsActive == true)
                IndicateDirection();
        }

        private void CreateTargetPointer(Transform target) {

            _targetPointer = Instantiate(_targetPointerPrefab,
                                         target.position,
                                         target.rotation,
                                         target);

            _targetPointer.Activate(false);
        }

        private void CreateNavigationPointer(Transform parent) {

            _navigationPointer = Instantiate(_navigationPointerPrefab,
                                             parent.position,
                                             parent.rotation,
                                             parent);

            _navigationPointer.Activate(false);
        }

        private void IndicateDirection() {

            if (_targetPointer == null) {
                _navigationPointer.transform.rotation = Quaternion.LookRotation(_parent.forward);
                return;
            }

            Vector3 toTarget = _targetPointer.transform.position - _parent.position;
            Vector3 toTargetXZ = new Vector3(toTarget.x, 0f, toTarget.z);

            _navigationPointer.transform.rotation = Quaternion.LookRotation(toTargetXZ);
        }
    }
}

