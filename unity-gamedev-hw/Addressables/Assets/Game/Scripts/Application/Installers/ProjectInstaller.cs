using AssetManagementSystem;
using UI.Services.Observers;
using UnityEngine;
using Zenject;

namespace SampleGame.Core {

    [CreateAssetMenu(
        fileName = nameof(ProjectInstaller),
        menuName = "Installers/New " + nameof(ProjectInstaller)
    )]
    public sealed class ProjectInstaller : ScriptableObjectInstaller {
        [Space, SerializeField] private SceneReferencesProvider _sceneRefProvider;

        public override void InstallBindings() {

            Container.Bind<ApplicationExiter>()
                .AsSingle()
                .NonLazy();

            Container.BindInstance(_sceneRefProvider)
                .AsSingle()
                .NonLazy();

            Container
                .Bind<ISceneLoader>()
                .To<SceneLoaderWithProgress>()
                .AsSingle()
                .WithArguments(_sceneRefProvider)
                .NonLazy();

            Container.Bind<MenuScreenObserver>()
                .AsSingle()
                .NonLazy();

            Container.Bind<GameplayScreenObserver>()
                .AsSingle()
                .NonLazy();

            Container.Bind<PauseScreenObserver>()
                .AsSingle()
                .NonLazy();
        }
    }
}