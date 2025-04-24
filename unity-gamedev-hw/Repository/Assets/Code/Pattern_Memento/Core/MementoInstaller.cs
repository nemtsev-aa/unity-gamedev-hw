using GameEngine;
using Unit_Spawn_System;
using UnityEngine;
using Zenject;

namespace Pattern_Memento {

    public class MementoInstaller : MonoInstaller {
        [SerializeField] private UnitManager _unitManager;
        [Space(10)]
        [SerializeField] private MomemtosPopup _unitsPopup;
        [SerializeField] private UnitManagerView _unitManagerView;
        [Space(20)]
        [SerializeField] private ResourceService _resourceService;
        [Space(10)]
        [SerializeField] private MomemtosPopup _resourcesPopup;
        [SerializeField] private ResourceServiceView _resourceServiceView;
        [Space(10)]
        [SerializeField] private CommonPopup _commonPopup;

        public override void InstallBindings() {
            BindUnitCompanents();
            BindResourceCompanents();
            BindFactory();
            BindServicesPresenter();
            BindCommonPopup();
        }

        private void BindUnitCompanents() {
            Container.BindInstance(_unitManager)
               .AsSingle()
               .NonLazy();

            Container.Bind<MomemtosPopup>()
                .WithId(MementoTypes.Units)
                .FromInstance(_unitsPopup);

            UnitManagerPresenter presenter = new UnitManagerPresenter(
                _unitManager,
                _unitManagerView,
                Container.Resolve<UnitSpawnArgsFactory>()
            );

            Container.BindInstance(presenter)
                .AsSingle();

            Container.Bind<IMementoHandler>()
                .WithId(MementoTypes.Units)
                .To<UnitMementoHandler>()
                .AsSingle();
        }

        private void BindResourceCompanents() {
            Container.BindInstance(_resourceService)
               .AsSingle()
               .NonLazy();

            Container.Bind<MomemtosPopup>()
                .WithId(MementoTypes.Resources)
                .FromInstance(_resourcesPopup);

            Container.Bind<ResourceServiceView>()
                .WithId(MementoTypes.Resources)
                .FromInstance(_resourceServiceView);

            ResourceServicePresenter presenter = new ResourceServicePresenter(
                _resourceService,
                _resourceServiceView
            );

            Container.BindInstance(presenter)
                .AsSingle();

            Container.Bind<IMementoHandler>()
                .WithId(MementoTypes.Resources)
                .To<ResourceMementoHandler>()
                .AsSingle();
        }

        private void BindFactory() {
            Container.Bind<IMementoServicesFactory>()
                .To<MementoServicesFactory>()
                .AsSingle();
        }

        private void BindServicesPresenter() {
            Container.Bind<MementoCoordinator>()
                .FromNew()
                .AsSingle();
        }

        private void BindCommonPopup() {
            Container.BindInstance(_commonPopup)
                .AsSingle();
        }
    }
}