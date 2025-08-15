using UI.Components.Screens;
using UnityEngine;
using Zenject;

namespace UI.Core {

    public class UISystemInstaller : MonoInstaller {
        [SerializeField] private UIManager _uiManagerPrefab;
        [SerializeField] private UIScreenConfigs _screenConfigs;
        [SerializeField] private UIProvidersInstaller _providersInstaller;

        public override void InstallBindings() {
            Container.BindInstance(_screenConfigs)
                     .AsSingle();
            
            var instance = Container.InstantiatePrefabForComponent<UIManager>(_uiManagerPrefab);

            Container.Bind<UIManager>()
                     .FromInstance(instance)    
                     .AsSingle()
                     .NonLazy();

            _providersInstaller.Install(Container);
        }
    }
}
