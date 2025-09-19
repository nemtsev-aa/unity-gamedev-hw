using R3;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using Tutorial.Core;

namespace Tutorial.UI {

    public sealed class TutorialStepInfoView : MonoBehaviour, IDisposable {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description; 
        [SerializeField] private Image _icon;

        private ITutorialStepInfoViewModel _viewModel;
        private CompositeDisposable _disposables = new();

        public void Init(ITutorialStepInfoViewModel viewModel) {
            _viewModel = viewModel;

            CreateReactiveSubscribes();
            Show(false);
        }

        public void Show(bool status) {
            gameObject.SetActive(status);
        }

        public void UpdateDescription(string text) {
            _description.text = text;
        }

        private void CreateReactiveSubscribes() {

            _viewModel.TutorialStepInfo
                .Subscribe(TutorialStepInfoChanged)
                .AddTo(_disposables);
        }

        private void TutorialStepInfoChanged(TutorialStepInfo info) {

            if (info == null ||
                info.Type == TutorialStep.Start ||
                info.Type == TutorialStep.Welcome || 
                info.Type == TutorialStep.Congratulate)
                return;

            Show(true);

            _name.text = $"Step {info.Id}";
            _description.text = info.Description;
            _icon.sprite = info.Icon;
        }

        public void Dispose() {

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();
        }
    }
}

