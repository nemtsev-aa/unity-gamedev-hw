using System;
using Zenject;
using UnityEngine;
using ProgressService;
using NavigatorService;
using BehaviorTree.PlayerCoreSubsystem;

namespace Tutorial.Core {

    [Serializable]
    public sealed class TutorialControllersInstaller {
        [SerializeField] private TutorialState _state;
        [SerializeField] private Navigator _navigator;
        [SerializeField] private Player _player;

        [Space, SerializeField] private TutorialMainController_Config _mainConfig;
        [Space, SerializeField] private ShowStartPopupStepController_Config _showStartPopupConfig;
        [Space, SerializeField] private TakeResourceStepController_Config _takeResourceConfig;
        [Space, SerializeField] private CollectLootStepController_Config _collectLootConfig;
        [Space, SerializeField] private SellResourceStepController_Config _sellResourceConfig;
        [Space, SerializeField] private CharacterUpgradeStepController_Config _characterUpgradeConfig;
        [Space, SerializeField] private KillEnemyStepController_Config _killEnemyConfig;
        [Space, SerializeField] private ShowFinishPopupStepController_Config _showFinishPopupConfig;

        private DiContainer _container;

        public void Install(DiContainer container) {

            _container = container;

            BindCommonComponents();
            BindTutorialMainController();
            BindShowStartPopupStepController();
            BindTakeResourceStepController();
            BindCollectLootStepController();
            BindSellResourceStepController();
            BindCharacterUpgradeStepController();
            BindKillEnemyStepController();
            BindShowFinishPopupStepController();

            container.Bind<TutorialControllersProvider>()
                     .AsSingle()
                     .NonLazy();
        }

        private void BindCommonComponents() {
            _container.BindInstance(_state)
                      .AsSingle()
                      .NonLazy();

            _container.BindInstance(_navigator)
                     .AsSingle()
                     .NonLazy();

            _container.Bind<PlayerProvider>()
                     .AsSingle()
                     .WithArguments(_player)
                     .NonLazy();
        }

        private void BindTutorialMainController() {
            _container.BindInstance(_mainConfig)
                     .AsSingle()
                     .NonLazy();

            _container.Bind<TutorialMainController>()
                     .AsSingle()
                     .NonLazy();
        }

        private void BindShowStartPopupStepController() {
            _container.BindInstance(_showStartPopupConfig)
                     .AsSingle()
                     .NonLazy();

            _container.Bind<ShowStartPopupStepController>()
                     .AsSingle()
                     .NonLazy();
        }

        private void BindTakeResourceStepController() {
            _container.BindInstance(_takeResourceConfig)
                     .AsSingle()
                     .NonLazy();

            _container.Bind<TakeResourceStepController>()
                     .AsSingle()
                     .NonLazy();
        }

        private void BindCollectLootStepController() {
            _container.BindInstance(_collectLootConfig)
                     .AsSingle()
                     .NonLazy();

            _container.Bind<CollectLootStepController>()
                     .AsSingle()
                     .NonLazy();
        }

        private void BindSellResourceStepController() {
            _container.BindInstance(_sellResourceConfig)
                     .AsSingle()
                     .NonLazy();

            _container.Bind<SellResourceStepController>()
                     .AsSingle()
                     .NonLazy();
        }

        private void BindCharacterUpgradeStepController() {
            _container.BindInstance(_characterUpgradeConfig)
                     .AsSingle()
                     .NonLazy();

            _container.Bind<CharacterUpgradeStepController>()
                     .AsSingle()
                     .NonLazy();
        }

        private void BindKillEnemyStepController() {
            _container.BindInstance(_killEnemyConfig)
                     .AsSingle()
                     .NonLazy();

            _container.Bind<KillEnemyStepController>()
                     .AsSingle()
                     .NonLazy();
        }

        private void BindShowFinishPopupStepController() {
            _container.BindInstance(_showFinishPopupConfig)
                     .AsSingle()
                     .NonLazy();

            _container.Bind<ShowFinishPopupStepController>()
                     .AsSingle()
                     .NonLazy();
        }
    }
}

