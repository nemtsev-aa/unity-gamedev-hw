using System;
using UI.Components.Screens;
using UI.Services.Observers;
using UnityEngine;
using Zenject;

namespace UI.Core {

    public class UISystemInstaller : MonoInstaller {
        [SerializeField] private UIManager _uiManagerPrefab;
        [SerializeField] private UIScreenConfigs _screenConfigs;

        public override void InstallBindings() {
            BindUIManager();
            BindUIScreenObservers();
        }

        private void BindUIManager() {
            Container.BindInstance(_screenConfigs)
                    .AsSingle();

            var instance = Container.InstantiatePrefabForComponent<UIManager>(_uiManagerPrefab);

            Container.Bind<UIManager>()
                     .FromInstance(instance)
                     .AsSingle()
                     .NonLazy();
        }

        private void BindUIScreenObservers() {
            Container.Bind<MenuScreenObserver>()
                .AsSingle()
                .NonLazy();

            Container.Bind<GameplayScreenObserver>()
                .AsSingle()
                .NonLazy();

            Container.Bind<PauseScreenObserver>()
                .AsSingle()
                .NonLazy();

            Container.Bind<GameoverScreenObserver>()
                .AsSingle()
                .NonLazy();
        }
    }
}
