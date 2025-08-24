using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Components.Screens {

    public class LoadingScreen : UIComponent {
        [SerializeField] private Slider _progressSlider;
        [SerializeField] private TextMeshProUGUI _loadingText;
        [SerializeField] private Button _cancelButton;
        [Space, SerializeField] private float _progressSpeed = 2f;

        private float _targetProgress;
        private bool _isCancellable;
        private CancellationTokenSource _cancelTokenSource;

        public event Action CancelRequested;

        protected override void OnInitialize() {
            _cancelButton.onClick.AddListener(OnCancelClicked);
            _cancelTokenSource = new CancellationTokenSource();
        }

        protected override async void OnShown() {
            ResetProgress();
            StartProgressAnimation().Forget();
        }

        protected override void OnHidden() {
            _cancelTokenSource?.Cancel();
        }

        public void SetLoadingText(string text) {

            //Debug.Log($"<color=yellow> {text} </color>");

            if (_loadingText != null)
                _loadingText.text = text;
        }

        public void SetCancellable(bool cancellable) {
            _isCancellable = cancellable;
            _cancelButton.gameObject.SetActive(cancellable);
        }

        public void SetProgress(float progress) {
            _targetProgress = Mathf.Clamp01(progress);
        }

        private async UniTaskVoid StartProgressAnimation() {

            while (IsActive == true) {
                if (_progressSlider.value < _targetProgress) {
                    _progressSlider.value = Mathf.MoveTowards(
                        _progressSlider.value,
                        _targetProgress,
                        Time.deltaTime * _progressSpeed
                    );
                }

                await UniTask.Yield(PlayerLoopTiming.Update, _cancelTokenSource.Token);
            }
        }

        public void ResetProgress() {
            _progressSlider.value = 0f;
            _targetProgress = 0f;
        }

        private void OnCancelClicked() {
            if (_isCancellable) {
                CancelRequested?.Invoke();
            }
        }

        protected override void OnDestroy() {
            _cancelTokenSource?.Dispose();
        }
    }
}