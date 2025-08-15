using LevelZoneSystem;
using UnityEngine;

namespace CharactersSystem.Player.Components {

    public sealed class LocationChecker : MonoBehaviour {
        public const float CHECK_PERIOD = 1f;
        public int CurrentZoneIndex { get; private set; } = 0;

        private float _time = 0;

        public void ResetZoneIndex() {
            CurrentZoneIndex = 0;
        }

        public void Update() {

            if (CurrentZoneIndex != 0)
                return;

            _time += Time.deltaTime;

            if (_time > CHECK_PERIOD) {
                _time = 0;

                if (TryGetCurrentZoneIndex(out int newZoneIndex) == false) {
                    CurrentZoneIndex = 0;
                    return;
                }

                CurrentZoneIndex = newZoneIndex;
            }
        }

        private bool TryGetCurrentZoneIndex(out int newZoneIndex) {

            Ray ray = new Ray(transform.position, -transform.up);

            if (Physics.Raycast(ray, out RaycastHit hit) == false) {
                newZoneIndex = 0;
                return false;
            }

            if (hit.collider.TryGetComponent(out ZoneProxy proxy) == false) {
                newZoneIndex = 0;
                return false;
            }

            newZoneIndex = proxy.Zone.Index;
            return true;
        }

        private void OnTriggerEnter(Collider other) {

            if (other.gameObject.TryGetComponent(out ZoneTriggerProxy proxy) == false)
                return;

            proxy.ZoneTrigger.Apply(CurrentZoneIndex);

            if (proxy.ZoneTrigger.Type == TransitionTriggerTypes.Finish)
                ResetZoneIndex();
        }
    }
}