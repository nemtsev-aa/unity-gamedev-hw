using UnityEngine;

namespace Tutorial.Core {
    public abstract class TutorialStateControllerBase : ITutorialStateController {
        public TutorialState TutorialState { get; protected set; }
        public TutorialStep TutorialStep { get; protected set; }

        protected bool HasStarted = false;

        public virtual void Init(TutorialState state) {
            TutorialState = state;
            TutorialState.StepStarted += OnStepStarted;
        }

        public virtual void OnStepStarted(TutorialStep tutorialStep) {

            if (tutorialStep == TutorialStep) {
                TutorialState.StepStarted -= OnStepStarted;
                Debug.Log($"Tutorial step [{TutorialStep}] started!");

                HasStarted = true;
                return;
            }
        }

        public virtual void Dispose() {
            
        }
    }
}