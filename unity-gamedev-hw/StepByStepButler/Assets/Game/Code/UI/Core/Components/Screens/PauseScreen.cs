using R3;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Components.Screens {

    public sealed class PauseScreen : UIComponent {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;

        public Observable<Unit> ResumeButtonClicked => _resumeButton.OnClickAsObservable();
        public Observable<Unit> RestartButtonClicked => _restartButton.OnClickAsObservable();
        public Observable<Unit> ExitButtonClicked => _exitButton.OnClickAsObservable();
    }
}