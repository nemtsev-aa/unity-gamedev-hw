using UnityEngine;

namespace TimeScalerService {
    
    public class UnityTimeService : ITimeService {
        public float TimeScale {
            get => Time.timeScale;
            set => Time.timeScale = value;
        }
    }
}