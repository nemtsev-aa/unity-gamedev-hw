using ShootEmUp;
using UnityEngine;
using Zenject;

public sealed class BulletSystemInstaller : MonoInstaller {
    [SerializeField] private BulletSystemConfig _config;

    public override void InstallBindings() {
        BindBulletFactory(_config.Prefab);
        BindBulletSpawner(_config.InitialCount);
        BindBulletSystem(_config.PositionCheckingInterval);
    }

    private void BindBulletFactory(Bullet prefab) {
        Container.Bind<BulletFactory>().AsSingle()
                 .WithArguments(prefab)
                 .NonLazy();
    }

    private void BindBulletSpawner(int initialCount) {
        Container.Bind<BulletSpawner>().AsSingle()
                 .WithArguments(initialCount)
                 .NonLazy();
    }

    private void BindBulletSystem(float positionCheckingInterval) {
        Container.Bind<BulletSystem>().AsSingle()
                 .WithArguments(positionCheckingInterval)
                 .NonLazy();
    }
}
