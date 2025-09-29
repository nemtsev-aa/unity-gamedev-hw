using R3;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Units.View {

    public sealed class UnitView : MonoBehaviour, IDisposable {
        public UnitViewModel ViewModel { get; private set; }
        public Subject<UnitView> ViewSelected = new Subject<UnitView>();

        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private Button _selector;

        private CompositeDisposable _disposables = new();
        private Observable<Unit> _selectorClicked => _selector.OnClickAsObservable();

        public void Init(UnitViewModel viewModel) {
            ViewModel = viewModel;

            _nameText.text = ViewModel.Name;
            _iconImage.sprite = ViewModel.Sprite;

            _selectorClicked
                .Subscribe(OnSelectorClicked)
                .AddTo(_disposables);
        }

        private void OnSelectorClicked(Unit unit) =>
            ViewSelected.OnNext(this);

        public void Dispose() {

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();
        }
    }
}