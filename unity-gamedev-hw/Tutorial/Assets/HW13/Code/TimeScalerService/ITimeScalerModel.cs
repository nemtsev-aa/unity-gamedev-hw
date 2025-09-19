namespace TimeScalerService {
    public interface ITimeScalerModel {
        float TimeScale { get; }
        void SetTimeScale(float value);
    }
}