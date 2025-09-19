using System;

namespace Tutorial.Core {

    public interface ITutorialStateController : IDisposable {
        TutorialState TutorialState { get; }
        TutorialStep TutorialStep { get; }
        void Init(TutorialState state);
        void OnStepStarted(TutorialStep tutorialStep);
    }
}