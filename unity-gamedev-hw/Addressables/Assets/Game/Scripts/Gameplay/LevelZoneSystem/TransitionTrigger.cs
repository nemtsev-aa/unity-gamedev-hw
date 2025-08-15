using System;
using UnityEngine;

namespace LevelZoneSystem {

    public enum TransitionTriggerTypes {
        Start = 0,
        Finish = 1
    }

    public sealed class TransitionTrigger : MonoBehaviour {
        public event Action<TransitionTriggerData> TriggerEntered;

        [field: SerializeField] public TransitionTriggerTypes Type { get; private set; }
        public int ZoneIndex { get; private set; }
        public bool IsActivate { get; private set; }

        public void Activate(int zoneIndex, bool status) {
            ZoneIndex = zoneIndex;
            IsActivate = status;
        }

        public void Apply(int characterCurrentZoneIndex) {

            if (IsActivate == true) {

                var data = new TransitionTriggerData {
                    CurrentZoneIndex = characterCurrentZoneIndex,
                    TriggerZoneTypes = Type,
                    TriggerZoneIndex = ZoneIndex
                };

                TriggerEntered?.Invoke(data);
            }
        }
    }
}