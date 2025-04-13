using R3;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PresentationModel {
    public sealed class PlayerLevelView : UIView, IDisposable {
        [SerializeField] private Image _notCompletedImage;
        [SerializeField] private Image _completedImage;

        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private TMP_Text _xpText;
        [SerializeField] private LevelUpButton _levelUpButton;

        private readonly CompositeDisposable _compositeDisposable = new();

        private PlayerLevelViewModel _viewModel;

        public override void Init(IViewModel viewModel) {
            
            if (viewModel is not PlayerLevelViewModel playerLevelViewModel)
                throw new ArgumentException($"Invalid ViewModel {viewModel}");

            _viewModel = playerLevelViewModel;

            UpdateCompanents();
            CreteReactiveSubscribes();
        }
       
        public override void UpdateCompanents() {
            int exp = _viewModel.Experience;
            int requiredExp = _viewModel.RequiredExperience;

            _levelText.text = $"Level: {_viewModel.Level}";
            _xpText.text = $"XP: {exp} / {requiredExp}";
            
            float fillAmount = Mathf.Clamp01((float)exp / requiredExp);
            _notCompletedImage.fillAmount = fillAmount;

            bool isComplete = fillAmount >= 1f;
            _completedImage.gameObject.SetActive(isComplete);
            _notCompletedImage.gameObject.SetActive(!isComplete);

            OnExperienceChanged(isComplete);
        }
        
        private void CreteReactiveSubscribes() {
            _levelUpButton.OnClickObservable
                .Subscribe(OnLevelUpButtonClicked)
                .AddTo(_compositeDisposable);

            _viewModel.CanLevelUp
                .Subscribe(OnExperienceChanged)
                .AddTo(_compositeDisposable);
        }

        private void OnLevelUpButtonClicked(Unit _) {
            _viewModel.LevelUp();
            UpdateCompanents();
        }

        private void OnExperienceChanged(bool canLevelUp) {
            
            LevelUpButtonState buttonState = canLevelUp ?
                LevelUpButtonState.Available :
                LevelUpButtonState.Locked;

            _levelUpButton.SetState(buttonState);
        }

        public void Dispose() {
            if (_compositeDisposable.IsDisposed == false)
                _compositeDisposable.Dispose();
        }
    }
}
