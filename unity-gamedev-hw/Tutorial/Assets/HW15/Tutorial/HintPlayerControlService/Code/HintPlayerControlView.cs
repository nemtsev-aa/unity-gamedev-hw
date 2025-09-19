using R3;
using TMPro;
using System;
using Zenject;
using UnityEngine;
using UnityEngine.UI;

namespace HintPlayerControlService {

    public sealed class HintPlayerControlView : MonoBehaviour, IDisposable {
        [SerializeField] private Image _actionImage;
        [SerializeField] private TMP_Text _labelText;

        private readonly CompositeDisposable _disposables = new();
        private IHintPlayerControlViewModel _viewModel;

        [Inject]
        public void Construct(IHintPlayerControlViewModel viewModel) {
            _viewModel = viewModel;

            CreateReactiveSubscribes();
        }

        private void CreateReactiveSubscribes() {

            _viewModel.CurrentData
                      .Subscribe(ShowCurrentHint)
                      .AddTo(_disposables);
        }

        private void ShowCurrentHint(HintPlayerControlConfig config) {
            _actionImage.sprite = config.Icon;
            _labelText.text = config.Description;
        }

        public void Dispose() {

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();
        }
    }
}