using R3;
using System;
using Tutorial.UI;
using Cysharp.Threading.Tasks;

namespace Tutorial.Core {

    [Serializable]
    public sealed class ShowFinishPopupStepController : TutorialStateControllerBase {
        private readonly TutorialFinishPopup _popup;
        private readonly CompositeDisposable _disposables = new();

        public ShowFinishPopupStepController(ShowFinishPopupStepController_Config config) {
            _popup = config.Popup;
            TutorialStep = TutorialStep.Congratulate;
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

            TutorialState.FinishStep();
            TutorialState.CompleteStep();
        }

        public override void Dispose() {

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();
        }
    }
}