using R3;
using Tutorial.Core;

namespace Tutorial.UI {

    public sealed class TutorialStepInfoViewModel : ITutorialStepInfoViewModel {
        public ReadOnlyReactiveProperty<TutorialStepInfo> TutorialStepInfo => _currentTutorialStepInfo;
        
        private readonly TutorialStepInfoConfig _configs;
        private ReactiveProperty<TutorialStepInfo> _currentTutorialStepInfo = new ReactiveProperty<TutorialStepInfo>();

        public TutorialStepInfoViewModel(TutorialStepInfoConfig configs) {
            _configs = configs;
        }

        public void SetTutorialStep(TutorialStep step) {

            if (_configs.TryGetInfoByType(step, out TutorialStepInfo info) == false)
                return;

            _currentTutorialStepInfo.Value = info;
        }
    }
}

