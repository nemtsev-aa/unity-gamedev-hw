using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Components.Screens {

    public sealed class GameoverScreen : UIComponent {
        public const string WINNER_LABEL = "WINNER: ";

        [SerializeField] private TMP_Text _winnerNameLabel;
        [SerializeField] private Button _restart;
        [SerializeField] private Button _mainMenu;

        public Observable<Unit> RestartButtonClicked => _restart.OnClickAsObservable();
        public Observable<Unit> MainMenuButtonClicked => _mainMenu.OnClickAsObservable();

        public void UpdateWinnerLabel(string winner) {
            _winnerNameLabel.text = $"{WINNER_LABEL} {winner}";
        }
    }
}