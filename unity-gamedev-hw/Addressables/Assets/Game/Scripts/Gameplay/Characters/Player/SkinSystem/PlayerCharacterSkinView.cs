using R3;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CharactersSystem.Player.Skins {
    public sealed class PlayerCharacterSkinView : MonoBehaviour, IDisposable {
        public Observable<int> Selected => _selected;
        public int Id => _viewModel.Id; 

        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private Image _icon;
        [SerializeField] private Button _selector;

        private Observable<R3.Unit> SelectorButtonClicked => _selector.OnClickAsObservable();
        private Subject<int> _selected = new();
        private CompositeDisposable _disposables = new();
        private IPlayerCharacterSkinViewModel _viewModel;

        public void Init(IPlayerCharacterSkinViewModel viewModel) {
            _viewModel = viewModel;

            _nameText.text = _viewModel.Name;
            _icon.sprite = _viewModel.Icon;

            SelectorButtonClicked
                .Subscribe(OnSelectorButtonClicked)
                .AddTo(_disposables);
        }

        private void OnSelectorButtonClicked(R3.Unit unit) {
            _selected.OnNext(_viewModel.Id);
        }

        public void Dispose() {

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();
        }
    }
}
