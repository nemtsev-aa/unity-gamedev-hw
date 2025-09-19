using R3;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UpgradesSystem.UI {

    public sealed class UpgradeView : MonoBehaviour, IDisposable {
        public const string STATE_LABEL = "Value: ";
        public const string LEVEL_LABEL = "Level: ";

        [SerializeField] private Image _iconImage;
        [Space, SerializeField] private TextMeshProUGUI _titleText;

        [FormerlySerializedAs("valueText")]
        [SerializeField] private TextMeshProUGUI _statsText;
        [SerializeField] private TextMeshProUGUI _levelText;
        [Space, SerializeField] private UpgradeButton _upgradeButton;

        private UpgradeViewModel _viewModel;
        private CompositeDisposable _disposables = new();

        public void Init(UpgradeViewModel viewModel) {
            _viewModel = viewModel;

            UpdateCompanents(true);
            CreateReactiveSubscribes();
        }

        private void UpdateCompanents(bool status) {

            if (status == false)
                return;

            _iconImage.sprite = _viewModel.Icon;
            _titleText.text = _viewModel.Name;
            _statsText.text = $"{STATE_LABEL} {_viewModel.Stats}";
            _levelText.text = $"{LEVEL_LABEL} {_viewModel.LevelInfo}";

            if (_viewModel.TryGetNextPrice(out int nextPrice) == true) {
                _upgradeButton.SetPrice($"{nextPrice}");
                return;
            }

            _upgradeButton.SetState(UpgradeButtonStates.MAX);
        }

        private void CreateReactiveSubscribes() {
            _upgradeButton.Button.OnClickAsObservable()
                .Subscribe(_ => _viewModel.UpgradeCommand.Execute(Unit.Default))
                .AddTo(_disposables);

            _viewModel.CanUpgrade
                .Subscribe(OnCanUpgrade)
                .AddTo(_disposables);

            _viewModel.OnLevelUp
                .Subscribe(UpdateCompanents)
                .AddTo(_disposables);
        }

        private void OnCanUpgrade(bool canUpgrade) {

            if (_upgradeButton.CurrentState == UpgradeButtonStates.MAX)
                return;
            
            var buttonState = canUpgrade ?
                UpgradeButtonStates.AVAILABLE :
                UpgradeButtonStates.LOCKED;

            _upgradeButton.SetState(buttonState);
        }

        public void Dispose() {

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();
        }
    }
}
