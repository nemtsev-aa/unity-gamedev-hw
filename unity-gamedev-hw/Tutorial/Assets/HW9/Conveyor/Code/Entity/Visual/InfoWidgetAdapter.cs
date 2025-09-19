using System;
using Elementary;
using Conveyors.Entity.Core;

namespace Conveyors.Entity.Visual {

    [Serializable]
    public sealed class InfoWidgetAdapter : IAwakeListener,
                                            IEnableListener,
                                            IDisableListener {
        private ITimer _workTimer;
        private InfoWidget _view;

        public void Construct(ITimer workTimer, InfoWidget view) {
            _workTimer = workTimer;
            _view = view;
        }

        void IAwakeListener.Awake() {
            _view.SetVisible(true);
            _view.ProgressBar.SetVisible(_workTimer.IsPlaying);
        }

        void IEnableListener.OnEnable() {
            _workTimer.OnStarted += OnWorkStarted;
            _workTimer.OnTimeChanged += OnWorkProgressChanged;
            _workTimer.OnFinished += OnWorkFinished;
        }

        void IDisableListener.OnDisable() {
            _workTimer.OnStarted -= OnWorkStarted;
            _workTimer.OnTimeChanged -= OnWorkProgressChanged;
            _workTimer.OnFinished -= OnWorkFinished;
        }

        private void OnWorkStarted() {
            _view.ProgressBar.SetVisible(true);
        }

        private void OnWorkProgressChanged() {
            _view.ProgressBar.SetProgress(_workTimer.Progress);
        }

        private void OnWorkFinished() {
            _view.ProgressBar.SetVisible(false);
        }
    }
}