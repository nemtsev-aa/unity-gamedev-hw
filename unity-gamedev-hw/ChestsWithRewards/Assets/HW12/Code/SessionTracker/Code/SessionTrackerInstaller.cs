using UnityEngine;
using Zenject;

namespace SessionTrackerSystem {

    public sealed class SessionTrackerInstaller : MonoInstaller {
        [SerializeField] private UICompanentsPresenter _popupPresenter;

        public override void InstallBindings() {

            Container.Bind<ISessionDataSaver>()
                .To<SessionDataSaver>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<SessionTracker>()
                     .AsSingle()
                     .NonLazy();

            Container.BindInstance(_popupPresenter)
                     .AsSingle()
                     .NonLazy();
            
            Container.Bind<ViewModelFactory>()
                     .AsSingle()
                     .NonLazy();

            Container.Bind<SessionTracker_UI>()
                     .AsSingle()
                     .NonLazy();
        }
    }
}


