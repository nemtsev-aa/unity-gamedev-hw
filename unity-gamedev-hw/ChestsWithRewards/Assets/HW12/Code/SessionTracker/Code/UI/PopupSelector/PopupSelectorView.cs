using GameCycleSystem;
using R3;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SessionTrackerSystem {

    public sealed class PopupSelectorView : MonoBehaviour, IDisposable {
        public Observable<Unit> HistoryButtonClick => _historyButton.OnClickAsObservable();
        public Observable<Unit> CurrentSessionButtonClick => _currentSessionButton.OnClickAsObservable();
        public Observable<Unit> ChestsButtonClick => _chestsButton.OnClickAsObservable();

        [SerializeField] private Button _historyButton;
        [SerializeField] private Button _currentSessionButton;
        [SerializeField] private Button _chestsButton;

        private IPopupSelectorViewModel _viewModel;
        private CompositeDisposable _disposables = new();

        public void Init(IPopupSelectorViewModel viewModel) {
            _viewModel = viewModel;

            _viewModel.CurrentGameState
                .Subscribe(CurrentGameStateChanged)
                .AddTo(_disposables);
        }

        private void CurrentGameStateChanged(GameStates states) {

            if (states == GameStates.WaitingToStart) {
                _currentSessionButton.interactable = false;
                return;
            }
            
            _currentSessionButton.interactable = true;
        }

        public void Show(bool status) {
            gameObject.SetActive(status);
        }

        public void Dispose() {

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();
        }
    }
}


