using System;
using UnityEngine;

namespace Tutorial.Core {

    [Serializable]
    public sealed class TutorialState {
        public event Action<TutorialStep> StepStarted;
        public event Action<TutorialStep> StepFinished;
        public event Action Completed;

        [field: SerializeField] public TutorialStep CurrentStep { get; private set; }

        public bool IsCompleted { get; private set; }

        public void NextStep() {
            CurrentStep++;

            if (CurrentStep == TutorialStep.End) {
                IsCompleted = true;
                Completed?.Invoke();
                
                return;
            }

            StepStarted?.Invoke(CurrentStep);
        }

        public void SetStartStep(TutorialStep startStep) =>
            CurrentStep = startStep;
        
        public void FinishStep() => StepFinished?.Invoke(CurrentStep);

        public void CompleteStep() => NextStep();
        
    }
}