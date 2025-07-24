using R3;
using BehaviorTree.Brain;
using BehaviorTree.Bot.UI;

namespace BehaviorTree.Bot {
    public sealed class BotStateViewModel : IBotStateViewModel {
        public ReadOnlyReactiveProperty<BotStateViewData> CurrentBotStateViewData => _currentViewData;

        private readonly ReactiveProperty<BotStateViewData> _currentViewData = new();
        private readonly ReactiveProperty<BotStates> _currentBotState = new();
        private readonly BotStateViewConfigs _config;
        private readonly CompositeDisposable _disposables = new();

        public BotStateViewModel(ReadOnlyReactiveProperty<BotStates> currentBotState, BotStateViewConfigs config) {
            _currentBotState = (ReactiveProperty<BotStates>)currentBotState;
            _config = config;

            _currentBotState
                .Subscribe(OnCurrentBotStateChanged)
                .AddTo(_disposables);
        }

        private void OnCurrentBotStateChanged(BotStates states) {
            var config = _config.GetConfigByType(states);

            _currentViewData.Value = new BotStateViewData {
                Description = config.Description,
                Icon = config.Icon
            };
        }

        public void Dispose() {

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();
        }
    }
}



