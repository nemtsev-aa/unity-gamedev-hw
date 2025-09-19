using UnityEngine;
using Tutorial.UI;
using FarmingSystem;
using NavigatorService;
using BehaviorTree.PlayerCompanents;
using BehaviorTree.PlayerCoreSubsystem;
using InteractionService;

namespace Tutorial.Core {

    public sealed class TakeResourceStepController : TutorialStateControllerBase {
        private const string LABEL = "Take Wood";

        private readonly Transform _target;
        private readonly int _felledWoodMaxCount;

        private readonly FellerCompanent _feller;
        private readonly InteractionHandler _interactionHandler;
        private readonly Navigator _navigator;
        private readonly TutorialStepInfoView _tutorialStepInfoView;

        private ResourceSpot _currentResourceSpot;
        private int _felledWoodCount = 0;

        public TakeResourceStepController(TakeResourceStepController_Config config,
                                          PlayerProvider playerComponents,
                                          Navigator navigator,
                                          TutorialStepInfoView tutorialStepInfoView) {

            _target = config.Target;
            _felledWoodMaxCount = config.FelledWoodMaxCount;

            _feller = playerComponents.Feller;
            _interactionHandler = playerComponents.InteractionHandler;
            _navigator = navigator;
            _tutorialStepInfoView = tutorialStepInfoView;

            TutorialStep = TutorialStep.TakeResource;
        }

        public override void Init(TutorialState state) {
            base.Init(state);
        }

        public override void OnStepStarted(TutorialStep step) {
            base.OnStepStarted(step);

            if (HasStarted == false)
                return;

            _interactionHandler.InteractionStarted += OnInteractionStarted;
            _interactionHandler.InteractionComplited += OnInteractionComplited;

            _navigator.SetTarget(_target);

            _tutorialStepInfoView.UpdateDescription($"{LABEL} {_felledWoodCount}/{_felledWoodMaxCount}");
        }

        private void OnInteractionStarted(InteractionSource source) {

            if (source is ResourceFarming farming) {
                _feller.Activate(true);

                _currentResourceSpot = farming.ResourceSpot;
                _currentResourceSpot.CurrentStateChanged += ResourceSpot_CurrentStateChanged;

                _feller.SetTarget(farming.ResourceSpot);
            }
        }

        private void OnInteractionComplited(InteractionSource source) {
            _feller.Activate(false);
        }

        private void ResourceSpot_CurrentStateChanged(ResourceSpot spot, bool status) {

            if (_currentResourceSpot != null && _currentResourceSpot == spot) {

                if (status == false) {
                    _felledWoodCount++;
                    _tutorialStepInfoView.UpdateDescription($"{LABEL} {_felledWoodCount}/{_felledWoodMaxCount}");
                    _currentResourceSpot.CurrentStateChanged -= ResourceSpot_CurrentStateChanged;
                    _currentResourceSpot = null;

                    CurrentFelledWoodCountChanged();
                }
            }
        }

        private void CurrentFelledWoodCountChanged() {
            _tutorialStepInfoView.UpdateDescription($"{LABEL} {_felledWoodCount}/{_felledWoodMaxCount}");

            if (_felledWoodCount >= _felledWoodMaxCount) {
                Dispose();

                TutorialState.FinishStep();
                TutorialState.NextStep();
            }
        }

        public override void Dispose() {
            _interactionHandler.InteractionStarted -= OnInteractionStarted;
            _interactionHandler.InteractionComplited -= OnInteractionComplited;
        }
    }
}