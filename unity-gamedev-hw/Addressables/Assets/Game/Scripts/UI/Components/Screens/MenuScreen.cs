using R3;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Components.Screens {

    public sealed class MenuScreen : UIComponent {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _exitButton;

        public Observable<Unit> StartButtonClicked => _startButton.OnClickAsObservable();
        public Observable<Unit> ExitButtonClicked => _exitButton.OnClickAsObservable();
    }
}