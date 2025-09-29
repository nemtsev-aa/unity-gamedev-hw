using GameplayCompanents;
using UnitPoolSystem;
using UnityEngine;
using Zenject;

namespace GameCycleSystem {
    public sealed class UnitPoolInstaller : MonoInstaller {
        [SerializeField] private UnitPrefabPresenter _unitPrefabPresenter;
        [SerializeField] private ContainersPresenter _containersPresenter;

        public override void InstallBindings() {

            Container.BindInstance(_unitPrefabPresenter)
                .AsSingle()
                .NonLazy();

            Container.BindInstance(_containersPresenter)
                .AsSingle()
                .NonLazy();

            Container.Bind<UnitFactory>()
                .AsSingle()
                .NonLazy();

            Container.Bind<UnitPool>()
                .WithId("ArcherPool")
                .AsTransient()
                .NonLazy();

            Container.Bind<UnitPool>()
                .WithId("SwordsManPool")
                .AsTransient()
                .NonLazy();
        }
    }
}