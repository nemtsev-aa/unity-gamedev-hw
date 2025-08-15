using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelZoneSystem {

    public sealed class TransitionToZone : MonoBehaviour, IDisposable {
        public event Action<int> LoadZoneRequested;
        public event Action<int> UnloadZoneRequested;

        public bool IsActivate { get; private set; }

        [SerializeField] private TransitionToZoneConfig _config;
        [SerializeField] private List<TransitionTrigger> _triggers;

        public void SetStatus(bool status) {
            IsActivate = status;

            if (IsActivate == true)
                Activate();
        }

        private void Activate() {

            if (_triggers == null || _triggers.Count == 0) {
                Debug.LogWarning("No ZoneTriggers assigned to ZoneManager");
                return;
            }

            for (int i = 0; i < _triggers.Count; i++) {
                var trigger = _triggers[i];

                if (trigger.Type == TransitionTriggerTypes.Start)
                    trigger.Activate(_config.FinishZone, true);

                if (trigger.Type == TransitionTriggerTypes.Finish)
                    trigger.Activate(_config.StartZone, true);

                trigger.TriggerEntered += OnTriggerEntered;
            }
        }

        private void OnTriggerEntered(TransitionTriggerData data) {

            if (data.CurrentZoneIndex == 0 || data.CurrentZoneIndex == data.TriggerZoneIndex) {
                UnloadZoneRequested?.Invoke(data.TriggerZoneIndex);
                return;
            }

            LoadZoneRequested?.Invoke(data.TriggerZoneIndex);
        }

        public void Dispose() {

            // Отписываемся от событий триггеров
            if (_triggers != null) {

                for (int i = 0; i < _triggers.Count; i++) {
                    var trigger = _triggers[i];

                    trigger.Activate(i, false);
                    trigger.TriggerEntered -= OnTriggerEntered;
                }
            }
        }
    }
}