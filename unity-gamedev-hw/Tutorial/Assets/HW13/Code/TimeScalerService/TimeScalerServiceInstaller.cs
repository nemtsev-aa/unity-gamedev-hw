using Zenject;
using UnityEngine;
using System;

namespace TimeScalerService {
    public sealed class TimeScalerServiceInstaller : MonoInstaller {
        [SerializeField] private float _defaultTimeScale = 1f;
        [SerializeField] private TimeScalerView _view;

        public override void InstallBindings() {

            if (_view == null)
                throw new ArgumentNullException($"TimeScalerServiceInstaller: TimeScalerView not found!");

            _view.Init();

            Container.Bind<ITimeService>()
                    .To<UnityTimeService>()
                    .AsSingle();

            Container.Bind<ITimeScalerModel>()
                     .To<TimeScalerModel>()
                     .AsSingle()
                     .OnInstantiated<TimeScalerModel>((ctx, model) => {
                         model.SetTimeScale(_defaultTimeScale);
                     });

            Container.Bind<ITimeScalerView>()
                    .FromInstance(_view)
                    .AsSingle();

            Container.Bind<TimeScalerController>()
                     .AsSingle()
                     .NonLazy();
        }
    }
}