using UnityEngine;
using Currencies.UI;
using CursorChangeService;
using BehaviorTree.PlayerCompanents;
using BehaviorTree.PlayerCoreSubsystem;
using NavigatorService;
using InputService;

namespace Tutorial.Core {
    public sealed class TutorialMainController : TutorialStateControllerBase {
        public bool IsStarted { get; private set; } = false;

        private readonly CurrencyProvider _currencyProvider;
        private readonly Player _player;
        private readonly MoveCompanent _mover;
        private readonly Navigator _navigator;
        private readonly InputHandler _inputHandler;
        private readonly CursorChangeMediator _cursorChangeMediator;

        public TutorialMainController(TutorialMainController_Config config,
                                      PlayerProvider playerComponents,
                                      Navigator navigator,
                                      InputHandler inputHandler,
                                      CursorChangeMediator cursorChangeMediator) {

            _currencyProvider = config.CurrencyProvider;
            _player = playerComponents.Player;
            _mover = playerComponents.Mover;
            _navigator = navigator;
            _inputHandler = inputHandler;
            _cursorChangeMediator = cursorChangeMediator;

            TutorialStep = TutorialStep.Start;
        }

        public override void Init(TutorialState state) {
            base.Init(state);
        }

        public void Update() {

            if (IsStarted == false)
                return;

            _navigator.ShowDirection();
            _cursorChangeMediator.ActivateRaycaster();
        }

        public override void OnStepStarted(TutorialStep step) {
            base.OnStepStarted(step);

            if (HasStarted == false) 
                return;

            _navigator.Init(_player.transform);
            _currencyProvider.Show(false);

            _inputHandler.Activate(true);
            _inputHandler.TargetPositionChanged += OnTargetPositionChanged;

            IsStarted = true;

            TutorialState.FinishStep();
            TutorialState.NextStep();
        }

        private void OnTargetPositionChanged(Vector3 targetPosition) {
            var moveStatus = _mover.GetMoveStatus(out var distanceToTarget);

            if (moveStatus == MoveStatus.PathComplited || moveStatus == MoveStatus.HasPath)
                return;

            _mover.MoveToTarget(targetPosition);
        }

        public override void Dispose() {
            _inputHandler.TargetPositionChanged -= OnTargetPositionChanged;
        }
    }
}