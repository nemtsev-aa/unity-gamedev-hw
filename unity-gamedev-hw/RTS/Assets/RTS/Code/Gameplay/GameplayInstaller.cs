using Zenject;
using UnityEngine;
using Client.Services;
using GameCycleSystem;
using Client.Installer;

namespace GameplayCompanents {

    public sealed class GameplayInstaller : MonoInstaller {
        [SerializeField] private EcsStartup _ecsStartup;
        [SerializeField] private TownHallProvider _townHallProvider;
        [SerializeField] private TeamMaterialProvider _materialProvider;

        public override void InstallBindings() {

            Container.BindInterfacesAndSelfTo<GameCycle>()
                .AsSingle()
                .NonLazy();

            Container.BindInstance(_ecsStartup)
                .AsSingle()
                .NonLazy();

            Container.Bind<GameMediator>()
                .AsSingle()
                .NonLazy();

            Container.Bind<TownHallDestroyObserver>()
                .AsSingle()
                .WithArguments(_townHallProvider)
                .NonLazy();

            Container.Bind<GameCycleInstaller>()
                .AsSingle()
                .NonLazy();

            Container.BindInstance(_materialProvider)
               .AsSingle()
               .NonLazy();
        }
    }
}