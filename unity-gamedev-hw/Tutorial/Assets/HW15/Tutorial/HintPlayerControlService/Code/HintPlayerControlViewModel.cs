using R3;
using CursorChangeService;
using System.Collections.Generic;

namespace HintPlayerControlService {
    public sealed class HintPlayerControlViewModel : IHintPlayerControlViewModel {
        public ReadOnlyReactiveProperty<HintPlayerControlConfig> CurrentData => _currentData;

        private ReactiveProperty<HintPlayerControlConfig> _currentData;
        private readonly List<HintPlayerControlConfig> _config;
        private readonly CursorChangeRaycaster _raycaster;
        private readonly CompositeDisposable _disposables = new();

        public HintPlayerControlViewModel(HintPlayerControlConfigs config,
                                          CursorChangeRaycaster raycaster) {

            _config = config.Configs;
            _raycaster = raycaster;
            _currentData = new ReactiveProperty<HintPlayerControlConfig>();

            CreateReactiveSubscribes();
        }

        private void CreateReactiveSubscribes() {

            _raycaster.CurrentCursorType
                      .Subscribe(ShowHintByType)
                      .AddTo(_disposables);
        }

        private void ShowHintByType(CursorType type) {

            if (type == CursorType.Default) {
                _currentData.Value = _config[0];
                return;
            }

            _currentData.Value = _config[1];
        }

        public void Dispose() {

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();
        }
    }
}