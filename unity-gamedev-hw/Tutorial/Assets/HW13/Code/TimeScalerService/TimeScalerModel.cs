using UnityEngine;

namespace TimeScalerService {

    public sealed class TimeScalerModel : ITimeScalerModel {
        public float TimeScale { get; private set; } = 1f;

        public void SetTimeScale(float value) {
            TimeScale = Mathf.Clamp(value, 1f, 10f);
        }
    }
}
