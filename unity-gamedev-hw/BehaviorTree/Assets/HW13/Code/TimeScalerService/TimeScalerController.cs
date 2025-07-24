using R3;
using System;

namespace TimeScalerService {

    public sealed class TimeScalerController : IDisposable {
        private readonly ITimeScalerModel _model;
        private readonly ITimeScalerView _view;
        private readonly ITimeService _timeService;

        private readonly CompositeDisposable _disposables = new();

        public TimeScalerController(ITimeService timeService,
                                    ITimeScalerView view, 
                                    ITimeScalerModel model) {
            _model = model;
            _view = view;
            _timeService = timeService;

            _view.TimeScaleValue
                .Subscribe(TimeScaleValueChanged)
                .AddTo(_disposables);
        }

        private void TimeScaleValueChanged(float timeScale) {
            _model.SetTimeScale(timeScale);
            _timeService.TimeScale = timeScale;
        }

        public void Dispose() {

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();
        }
    }
}