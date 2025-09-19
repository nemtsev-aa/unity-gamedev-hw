using R3;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial.UI {

    public class Popup : MonoBehaviour, IDisposable {
        public ReadOnlyReactiveProperty<bool> Status => ShowStatus;
        public Observable<Unit> ApplyButtonClicked => _applyButton.OnClickAsObservable();
        public Observable<Unit> CloseButtonClicked => _closeButton.OnClickAsObservable();

        [SerializeField] private Button _applyButton;
        [SerializeField] private Button _closeButton;

        protected CompositeDisposable Disposables = new();
        protected ReactiveProperty<bool> ShowStatus;

        public virtual void Init() {
            ShowStatus = new ReactiveProperty<bool>(false);
            CreateReactiveSubscribes();
        }

        public virtual void Show(bool status) {
            ShowStatus.Value = status;
            gameObject.SetActive(status);
        }

        protected virtual void CreateReactiveSubscribes() {

            CloseButtonClicked
                .Subscribe(OnCloseButtonClicked)
                .AddTo(Disposables);
        }

        private void OnCloseButtonClicked(Unit _) {
            Show(false);
        }

        public virtual void Dispose() {

            if (Disposables.IsDisposed == false)
                Disposables.Dispose();
        }
    }
}

