using Zenject;
using UnityEngine;
using SceneManagementSystem;

namespace StepByStepButler.Core {

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
                .To<SceneLoader>()
                .AsSingle()
                .WithArguments(_sceneRefProvider)
                .NonLazy();
        }
    }
}