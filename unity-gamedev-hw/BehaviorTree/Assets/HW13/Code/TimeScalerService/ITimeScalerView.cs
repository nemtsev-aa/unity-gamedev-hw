using R3;
using System;

namespace TimeScalerService {
    public interface ITimeScalerView : IDisposable {
        ReadOnlyReactiveProperty<float> TimeScaleValue { get; }
        void SetScrollValue(float value);
    }
}