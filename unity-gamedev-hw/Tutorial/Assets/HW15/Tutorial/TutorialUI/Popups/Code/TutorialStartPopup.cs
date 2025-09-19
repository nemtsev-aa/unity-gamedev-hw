using R3;
using Cysharp.Threading.Tasks;

namespace Tutorial.UI {

    public sealed class TutorialStartPopup : Popup {

        protected override void CreateReactiveSubscribes() {
            base.CreateReactiveSubscribes();

            ApplyButtonClicked
                .Subscribe(OnApplyButtonClicked)
                .AddTo(Disposables);
        }

        private void OnApplyButtonClicked(Unit _) {
            Show(false);
        }
    }
}

