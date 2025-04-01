using ShootEmUp;
using UnityEngine;
using Zenject;

public sealed class SceneCompanentsInstaller : MonoInstaller {
    [SerializeField] private ContainersPresenter _containersPresenter;
    [Space(10)]
    [SerializeField] private LevelBackground _levelBackground;
    [SerializeField] private LevelBounds _levelBounds;

    public override void InstallBindings() {
        Container.Bind<InputManager>().AsSingle();
        Container.BindInstance(_levelBackground).AsSingle();
        Container.BindInstance(_levelBounds).AsSingle();
        Container.BindInstance(_containersPresenter).AsSingle();
    }
}
