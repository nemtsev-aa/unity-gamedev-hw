using R3;
using System;
using TMPro;
using UnityEngine;

namespace PresentationModel {
    public sealed class CharacterStatView : UIView, IDisposable {
        [SerializeField] private TMP_Text _descriptionText;

        private readonly CompositeDisposable _disposables = new();
        private ICharacterStatViewModel _characterStatViewModel;

        public string Name { get; private set; }

        public override void Init(IViewModel viewModel) {
            if (viewModel is not ICharacterStatViewModel characterStatViewModel)
                throw new ArgumentException($"Invalid ViewModel: {viewModel}");

            _characterStatViewModel = characterStatViewModel;

            Name = _characterStatViewModel.Name;

            CreateRactiveSubscribe();
            UpdateCompanents();
        }

        public override void UpdateCompanents() {
            _descriptionText.text = $"{_characterStatViewModel.Name}: {_characterStatViewModel.Value.CurrentValue}";
        }

        private void CreateRactiveSubscribe() {
            _characterStatViewModel.Value
                .Subscribe(UpdateValue)
                .AddTo(_disposables);
        }

        private void UpdateValue(int newValue) {
            _descriptionText.text = $"{Name}: {newValue}";
        }

        public void Dispose() {
            if (_disposables.IsDisposed == false) {
                _disposables.Dispose();
                _characterStatViewModel = null;
            }
        }

    }
}