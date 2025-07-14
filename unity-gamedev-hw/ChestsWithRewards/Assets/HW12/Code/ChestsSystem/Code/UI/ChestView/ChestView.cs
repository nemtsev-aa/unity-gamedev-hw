using R3;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace ChestsSystem {

    public sealed class ChestView : MonoBehaviour, IDisposable {
        public Observable<Unit> OpenChestButtonClicked => _openChestButton.OnClickAsObservable();
        public Subject<ReactiveChest> OpenAnimationFinished = new Subject<ReactiveChest>();

        [SerializeField] private TMP_Text _typeText;
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private Button _openChestButton;

        private readonly CompositeDisposable _disposables = new();

        private ReactiveChest _reactiveChest;
        private ChestVisual _visual;
        private Tween _openAnimation;

        public void Init(IChestViewModel viewModel) {
            _reactiveChest = viewModel.ReactiveChest;
            _visual = viewModel.Visual;

            CreateReactiveSubscribes();

            _typeText.text = _reactiveChest.Type.ToString();
            OnRemainingTime(_reactiveChest.RemainingTime.CurrentValue);
        }

        public void Show(bool status) {
            gameObject.SetActive(status);
        }

        private void CreateReactiveSubscribes() {
            _reactiveChest.IsReadyToOpen
                .Subscribe(OnIsReadyToOpen)
                .AddTo(_disposables);

            _reactiveChest.RemainingTime
                .Subscribe(OnRemainingTime)
                .AddTo(_disposables);

            OpenChestButtonClicked
                .Subscribe(StartOpenAnimation)
                .AddTo(_disposables);
        }

        private void OnIsReadyToOpen(bool status) {
            _openChestButton.interactable = status;

            if (status == true)
                _timerText.text = $"Ready To Open!";
        }

        private void OnRemainingTime(TimeSpan time) {

            if (time == TimeSpan.Zero && _reactiveChest.IsReadyToOpen.CurrentValue == true) {
                _timerText.text = $"Ready To Open!";
                return;
            }

            _timerText.text = FormatDuration(time);
        }

        private void StartOpenAnimation(Unit _) {
            _openAnimation?.Kill();
            var transform = _visual.TopPart.transform;

            _openAnimation = DOTween.Sequence()
                .Append(transform.DOLocalMoveY(100f, 1f))
                .AppendInterval(1f)
                .OnComplete(() => {
                    transform.DOLocalMoveY(70f, 0.1f);
                    OpenAnimationFinished.OnNext(_reactiveChest);
                })
                .Play();
        }

        private string FormatDuration(TimeSpan duration) {
            return $"{(int)duration.TotalHours:00}:{duration.Minutes:00}:{duration.Seconds:00}";
        }

        public void Dispose() {

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();
        }
    }
}
