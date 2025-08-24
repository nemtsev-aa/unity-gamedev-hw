using UI;
using Zenject;
using UnityEngine;
using EventBusService;
using StepByStepButler.Core;
using StepByStepButler.Gameplay.Heroes;
using StepByStepButler.Gameplay.Systems;
using StepByStepButler.Gameplay.Systems.Audio;
using UI.Components.ActiveEffectViewSystem;
using UI.Components.DamagePopupSystem;


namespace StepByStepButler.Gameplay {

    public sealed class GameplayInstaller : MonoInstaller {
        [SerializeField] private HeroIconsConfiguration _heroIconsConfig;
        [SerializeField] private HeroAudioConfiguration _heroAudioConfig;
        [SerializeField] private UIService _uiService;
        [SerializeField] private AudioPlayer _audioPlayer;
        [SerializeField] private DeathAnimationSettings _deathAnimationSettings;
        [Space, SerializeField] private DamagePopupView _damagePopupPrefab;
        [SerializeField] private Transform _popupParent;
        [SerializeField] private Camera _camera;
        [Space, SerializeField] private ActiveEffectViewConfigs _activeEffectViewConfigs;
        [SerializeField] private Transform _activeEffectParent;

        public override void InstallBindings() {
            BindSettings();
            BindEventBus();
            BindEntityFactory();
            BindSystems();
            BindGameplay();
        }

        private void BindSettings() {
            Container.BindInstance(_deathAnimationSettings);
        }

        private void BindEventBus() {

            Container.Bind<IEventBus>()
                .To<EventBus>()
                .AsSingle();
        }

        private void BindEntityFactory() {

            Container.Bind<EntityFactory>()
                .AsSingle();

        }

        private void BindSystems() {

            Container.BindInterfacesAndSelfTo<DeathAnimationSystem>()
              .AsSingle()
              .WithArguments(_deathAnimationSettings);

            Container.BindInterfacesAndSelfTo<HeroesProvider>()
                .AsSingle()
                .NonLazy();

            Container.BindInstance(_uiService)
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<TurnSystem>()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<CombatSystem>()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<ViewSystem>()
               .AsSingle()
               .WithArguments(_heroIconsConfig);

            Container.BindInterfacesAndSelfTo<AudioSystem>()
                .AsSingle()
                .WithArguments(_heroAudioConfig, _audioPlayer);

            BindDamagePopupSystem();
            BindActiveEffectViewSystem();
        }

        private void BindGameplay() {
            Container.BindInterfacesAndSelfTo<GameplayMediator>()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<GameplayInitializer>()
                .AsSingle()
                .NonLazy();
        }

        private void BindDamagePopupSystem() {
            Container.BindInterfacesAndSelfTo<DamageSystem>()
               .AsSingle();

            Container.Bind<DamagePopupPool>()
                .AsSingle()
                .WithArguments(_damagePopupPrefab, _popupParent);

            Container.BindInterfacesAndSelfTo<DamagePopupSystem>()
                .AsSingle();
        }

        private void BindActiveEffectViewSystem() {

            Container.Bind<ActiveEffectViewFactory>()
                .AsSingle()
                .WithArguments(_activeEffectViewConfigs, _activeEffectParent);

            Container.Bind<ActiveEffectViewPool>()
                .AsSingle()
                .WithArguments(_activeEffectViewConfigs);

            Container.Bind<ActiveEffectViewSystem>()
                .AsSingle()
                .NonLazy();
        }
    }
}