using Atomic.Contexts;
using System;
using UnityEngine;
using ZombieShooter.SceneObjects;

namespace AtomicFramework.EnemySystem {

    [Serializable]
    public sealed class EnemySystemInstaller : IContextInstaller {
        [SerializeField] private EnemySystemConfig _systemConfig;
        [SerializeField] private EnemyPositions _positions;
        [SerializeField] private EnemyConfig _config;

        public void Install(IContext context) {
            context.AddEnemySystemConfig(_systemConfig);
            context.AddEnemyPositions(_positions);
            context.AddEnemyConfig(_config);

            context.AddSystem<EnemyFactory>();
            context.AddSystem<EnemyPool>();
            context.AddSystem<EnemyManager>();
        }
    }
}
