using BehaviorTree.PlayerCoreSubsystem;
using System;
using Tutorial.UI;
using Progress = ProgressService.Progress;

namespace Tutorial.Core {

    [Serializable]
    public sealed class TutorialStateRunner : IDisposable {
        private readonly PlayerProvider _playerProvider;
        private readonly TutorialState _tutorialState;
        private readonly TutorialControllersProvider _controllerProvider;
        private readonly TutorialStepInfoViewModel _viewModel;

        private TutorialMainController _mainController;
        private ITutorialStateController _currentController;

        public TutorialStateRunner(PlayerProvider playerProvider,
                                   TutorialState tutorialState,
                                   TutorialControllersProvider provider,
                                   TutorialStepInfoViewModel viewModel) {

            _playerProvider = playerProvider;
            _tutorialState = tutorialState;
            _controllerProvider = provider;
            _viewModel = viewModel;
        }

        public void Start() {

            foreach (var controller in _controllerProvider.Controllers.Values) {

                if (controller != null)
                    controller.Init(_tutorialState);
            }

            if (_controllerProvider.Controllers.TryGetValue(TutorialStep.Start, out var mainController) == true)
                _mainController = (TutorialMainController)mainController;

            _tutorialState.StepStarted += OnStepStarted;

            if (_tutorialState.IsCompleted == false) 
                StartCurrentStep();
        }

        private void StartCurrentStep() {
            var loadedStepIndex = _playerProvider.ProgressData.Chapter;

            if (loadedStepIndex == 0) {
                _tutorialState.NextStep();
                return;
            }

            foreach (TutorialStep step in Enum.GetValues(typeof(TutorialStep))) {
                int numericValue = (int)step;
                int nearestPreviousStep = numericValue - 1;
                
                if (loadedStepIndex != numericValue)
                    continue;
                
                if (Enum.IsDefined(typeof(TutorialStep), nearestPreviousStep) == true) {
                    _tutorialState.SetStartStep((TutorialStep)nearestPreviousStep);
                    _mainController.OnStepStarted(TutorialStep.Start);

                    return;
                }
            }
        }

        private void OnStepStarted(TutorialStep step) {

            if (_controllerProvider.Controllers.TryGetValue(step, out var controller) == true) {
                _currentController = controller;
                
                int stepNumericValue = (int)step;
                _playerProvider.ProgressData.Chapter = stepNumericValue;
                _playerProvider.MarkDataChanged();

                //Debug.Log($"CurrentController: {_currentController.TutorialStep}");
            }

            _viewModel.SetTutorialStep(step);
        }

        public void Update() {

            if (_mainController != null)
                _mainController.Update();
        }

        public void Dispose() {
            _tutorialState.StepStarted -= OnStepStarted;
        }
    }
}