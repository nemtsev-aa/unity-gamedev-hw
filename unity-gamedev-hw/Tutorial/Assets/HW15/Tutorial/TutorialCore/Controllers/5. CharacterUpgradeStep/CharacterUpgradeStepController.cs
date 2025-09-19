using R3;
using System;
using Tutorial.UI;
using UnityEngine;
using Currencies.UI;
using UpgradesSystem.UI;
using BehaviorTree.PlayerCoreSubsystem;
using UpgradesUICompanents = UpgradesSystem.UI.UICompanents;
using NavigatorService;
using InteractionService;
using InputService;
using Tutorial.PlayerUpgrades;
using BehaviorTree.PlayerCompanents;
using ProgressService;

namespace Tutorial.Core {

    [Serializable]
    public sealed class CharacterUpgradeStepController : TutorialStateControllerBase {
        private readonly Transform _target;
        private readonly string _name;
        private readonly int _maxLevel;
        private readonly CurrencyProvider _currencyProvider;
        private readonly PlayerProvider _playerProvider;
        private readonly InteractionHandler _interactionHandler;
        private readonly MoveCompanent _mover;
        private readonly Navigator _navigator;
        private readonly TutorialStepInfoView _tutorialStepInfoView;
        private readonly CompositeDisposable _disposables = new();

        private UpgradeViewModel _upgradeViewModel;
        private TutorialCharacterUpgradePopup _upgradesPopup;
        private UpgradesPopupViewModelFactory _uPVMFactory;
        private TutorialPointerUI _upgradesPopupPointer;
        private UpgradesPopupViewModel _upgradesPopupViewModel;
        private PlayerProgressData _playerProgressData => _playerProvider.ProgressData;

        private int _currentUpgradeLevel;

        public CharacterUpgradeStepController(CharacterUpgradeStepController_Config config,
                                              PlayerProvider playerComponents,
                                              Navigator navigator,
                                              TutorialStepInfoView tutorialStepInfoView,
                                              UpgradesUICompanents upgradesUICompanents) {
            _target = config.Target;
            _name = config.UpgradeName;
            _maxLevel = config.UpgradeMaxLevel;
            _currencyProvider = config.CurrencyProvider;

            _playerProvider = playerComponents;
            _interactionHandler = playerComponents.InteractionHandler;
            _mover = playerComponents.Mover;

            _navigator = navigator;

            _tutorialStepInfoView = tutorialStepInfoView;
            _upgradesPopup = (TutorialCharacterUpgradePopup)upgradesUICompanents.Popup;
            _uPVMFactory = upgradesUICompanents.ViewModelFactory;
            _upgradesPopupPointer = _upgradesPopup.Pointer;

            TutorialStep = TutorialStep.UpgradeCharacters;
        }

        public override void Init(TutorialState state) {
            base.Init(state);
        }

        public override void OnStepStarted(TutorialStep step) {
            base.OnStepStarted(step);

            if (HasStarted == false)
                return;

            _navigator.SetTarget(_target);
            _currencyProvider.Show(true);
            InitUpgradesPopup();

            _interactionHandler.InteractionStarted += OnInteractionStarted;

            _tutorialStepInfoView.UpdateDescription($"Update Complited {_currentUpgradeLevel}/{_maxLevel}");
        }

        private void InitUpgradesPopup() {
            _upgradesPopupViewModel = _uPVMFactory.Get();
            _upgradesPopup.Init(_upgradesPopupViewModel);

            if (_upgradesPopupViewModel.TryGetUpgradeViewModelByName(_name, out var upgradeViewModel) == true) {
                _upgradeViewModel = upgradeViewModel;

                _upgradeViewModel.OnLevelUp
                    .Subscribe(UpgradeLevelUp)
                    .AddTo(_disposables);
            }

            _upgradesPopup.CloseButtonClicked
                .Subscribe(UpgradesPopup_CloseButtonClicked)
                .AddTo(_disposables);

            _upgradesPopupPointer.Init();
            _upgradesPopupPointer.AnimateCompleted += Pointer_AnimateCompleted;
        }

        private void OnInteractionStarted(InteractionSource source) {

            if (source is CharacterUpgradeStarter starter) {
                InputController.BlockInput(this);
                _upgradesPopup.Show(true);
                _upgradesPopupPointer.AnimateObject();

                return;
            }
        }

        private void UpgradeLevelUp(bool status) {

            if (status == true) {
                _currentUpgradeLevel = _upgradeViewModel.CurrentLevel;
                //Debug.Log($"CharacterUpgradeStepController: CurrentUpgradeLevel - {_currentUpgradeLevel}");
            }
        }

        private void UpgradesPopup_CloseButtonClicked(Unit _) {
            _mover.SetMoveSpeed(_playerProgressData.SpeedLevel);

            if (_currentUpgradeLevel == _maxLevel) {
                Dispose();

                TutorialState.FinishStep();
                TutorialState.CompleteStep();
            }
        }

        private void Pointer_AnimateCompleted() {
            InputController.UnblockInput(this);
        }

        public override void Dispose() {

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();
        }
    }
}