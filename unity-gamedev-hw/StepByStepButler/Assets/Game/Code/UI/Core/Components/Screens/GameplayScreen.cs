using R3;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Components.Screens {

    public sealed class GameplayScreen : UIComponent {
        [SerializeField] private Button _pauseButton;

        public Observable<Unit> PauseButtonClicked => _pauseButton.OnClickAsObservable();
    }
}