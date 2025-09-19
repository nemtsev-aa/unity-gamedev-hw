using System;
using Characters;
using UnityEngine;
using BehaviorTree.PlayerCompanents;
using BehaviorTree.PlayerCoreSubsystem;
using NavigatorService;
using InteractionService;
using ProgressService;

namespace Tutorial.Core {

    [Serializable]
    public sealed class KillEnemyStepController : TutorialStateControllerBase {
        private readonly Transform _target;
        private readonly PlayerProvider _playerProvider;
        private readonly InteractionHandler _interactionHandler;
        private readonly FellerCompanent _feller;
        private readonly Navigator _navigator;
        private PlayerProgressData _progressData => _playerProvider.ProgressData;

        private TrainingDummy _currentEnemy;

        public KillEnemyStepController(KillEnemyStepController_Config config,
                                       PlayerProvider playerProvider,
                                       Navigator navigator) {

            _target = config.Target;

            _playerProvider = playerProvider;
            _feller = playerProvider.Feller;
            _interactionHandler = playerProvider.InteractionHandler;

            _navigator = navigator;

            TutorialStep = TutorialStep.KillEnemy;
        }

        public override void Init(TutorialState state) {
            base.Init(state);
        }

        public override void OnStepStarted(TutorialStep step) {
            base.OnStepStarted(step);

            if (HasStarted == false)
                return;

            _navigator.SetTarget(_target);

            if (_target.TryGetComponent(out TrainingDummy trainingDummy) == true) {
                _currentEnemy = trainingDummy;
                _currentEnemy.SetState(CharacterStates.Idle);

                _currentEnemy.Destroyed += CurrentEnemy_Destroyed;
            }

            _interactionHandler.InteractionStarted += OnInteractionStarted;
        }

        private void OnInteractionStarted(InteractionSource source) {

            if (source is EnemyFightStarter starter) {

                if (_currentEnemy != null)
                    _feller.Activate(true);
                    _feller.SetTarget(_currentEnemy);
                    _currentEnemy.Extract(_progressData.DamageLevel);

                return;
            }
        }

        private void CurrentEnemy_Destroyed() {
            Dispose();

            TutorialState.FinishStep();
            TutorialState.CompleteStep();
        }

        public override void Dispose() {
            _currentEnemy.Destroyed -= CurrentEnemy_Destroyed;
            _interactionHandler.InteractionStarted -= OnInteractionStarted;
        }
    }
}