using R3;
using Tutorial.Core;

namespace Tutorial.UI {

    public interface ITutorialStepInfoViewModel {
        ReadOnlyReactiveProperty<TutorialStepInfo> TutorialStepInfo { get; }
        void SetTutorialStep(TutorialStep step);
    }
}

