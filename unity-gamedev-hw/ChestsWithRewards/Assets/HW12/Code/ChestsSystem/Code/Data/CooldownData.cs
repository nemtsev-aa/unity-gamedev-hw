using System;
using UnityEngine;

namespace ChestsSystem {

    [Serializable]
    public sealed class CooldownData {
        [field: SerializeField] public int Days { get; private set; }
        [field: SerializeField] public int Hours { get; private set; }
        [field: SerializeField] public int Minutes { get; private set; }
        [field: SerializeField] public int Seconds { get; private set; }

        public CooldownData(TimeSpan timeSpan) {
            Days = timeSpan.Days;
            Hours = timeSpan.Hours;
            Minutes = timeSpan.Minutes;
            Seconds = timeSpan.Seconds;
        }

        public TimeSpan GetTimeSpan() {
            return new TimeSpan(Days, Hours, Minutes, Seconds);
        }
    }
}
