using ShootEmUp;
using UnityEngine;
using Zenject;

public sealed class EnemySystemInstaller : MonoInstaller {
    [SerializeField] private EnemySystemConfig _systemConfig;
    [SerializeField] private EnemyConfig _config;
    [SerializeField] private EnemyPositions _positions;

    public override void InstallBindings() {
        BindConfigs();
        BindEnemyFactory(_systemConfig.Prefab);
        BindEnemyPool(_systemConfig.PoolSize);
        BindEnemyManager(_systemConfig.SpawnDelay);
    }

    private void BindConfigs() {
        Container.BindInstance(_config).AsSingle();
        Container.BindInstance(_positions).AsSingle();
    }

    private void BindEnemyFactory(Enemy prefab) {
        Container.Bind<EnemyFactory>().AsSingle()
         .WithArguments(prefab)
         .NonLazy();
    }

    private void BindEnemyPool(int poolSize) {
        Container.Bind<EnemyPool>().AsSingle()
                 .WithArguments(poolSize)
                 .NonLazy();
    }

    private void BindEnemyManager(float spawnDelay) {
        Container.Bind<EnemyManager>().AsSingle()
                 .WithArguments(spawnDelay)
                 .NonLazy();
    }
}
