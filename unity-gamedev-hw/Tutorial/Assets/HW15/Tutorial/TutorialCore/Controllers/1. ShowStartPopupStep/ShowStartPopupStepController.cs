using R3;
using System;
using Tutorial.UI;
using Cysharp.Threading.Tasks;

namespace Tutorial.Core {

    [Serializable]
    public sealed class ShowStartPopupStepController : TutorialStateControllerBase {

        private readonly TutorialStartPopup _popup;
        private CompositeDisposable _disposables = new();

        public ShowStartPopupStepController(ShowStartPopupStepController_Config config) {
            _popup = config.Popup;
            TutorialStep = TutorialStep.Welcome;
        }

        public override void Init(TutorialState state) {
            base.Init(state);
                
            _popup.Init();
            CreateReactiveSubscribes();
        }

        public override void OnStepStarted(TutorialStep step) {
            base.OnStepStarted(step);

            if (HasStarted == false)
                return;

            _popup.Show(true);
        }

        private void CreateReactiveSubscribes() {

            _popup.ApplyButtonClicked
                  .Subscribe(OnApplyButtonClicked)
                  .AddTo(_disposables);
        }

        private void OnApplyButtonClicked(Unit _) {
            _popup.Show(false);

            Dispose();

            TutorialState.FinishStep();
            TutorialState.CompleteStep();
        }

        public override void Dispose() {

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();
        }
    }
}