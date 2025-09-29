using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameCycleSystem {

    public sealed class GameCycleView : MonoBehaviour {
        private const string PAUSE_TEXT = "Pause";
        private const string RETURN_TEXT = "Return";

        public Observable<Unit> StartButtonClicked => _startButton.OnClickAsObservable();
        public Observable<Unit> PauseButtonClicked => _pauseButton.OnClickAsObservable();
        public Observable<Unit> FinishButtonClicked => _finishButton.OnClickAsObservable();

        [SerializeField] private Button _startButton;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _finishButton;

        private GameObject _startButtonGO;
        private GameObject _pauseButtonGO;
        private GameObject _finishButtonGO;

        private void Start() {
            _startButtonGO = _startButton.gameObject;
            _pauseButtonGO = _pauseButton.gameObject;
            _finishButtonGO = _finishButton.gameObject;
        }

        public void ShowDefaultState() {
            _startButtonGO.SetActive(true);
            _pauseButtonGO.SetActive(false);
            _finishButtonGO.SetActive(false);
        }

        public void ShowGameplayState() {
            _startButtonGO.SetActive(false);

            _pauseButtonGO.GetComponentInChildren<TMP_Text>()
                          .text = PAUSE_TEXT;

            _pauseButtonGO.SetActive(true);
            _finishButtonGO.SetActive(false);
        }

        public void ShowPauseState() {
            _startButtonGO.SetActive(false);

            _pauseButtonGO.GetComponentInChildren<TMP_Text>()
                          .text = RETURN_TEXT;

            _pauseButtonGO.gameObject.SetActive(true);
            _finishButtonGO.SetActive(false);
        }

        public void ShowFinishState() {
            _startButtonGO.SetActive(false);
            _pauseButtonGO.SetActive(false);
            _finishButtonGO.SetActive(true);
        }
    }
}