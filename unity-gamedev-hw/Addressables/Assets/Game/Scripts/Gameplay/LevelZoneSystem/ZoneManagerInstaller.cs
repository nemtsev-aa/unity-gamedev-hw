using SampleLevelZoneSystemGame;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LevelZoneSystem {

    public sealed class ZoneManagerInstaller : MonoInstaller {
        [SerializeField] private Transform _worldRoot;
        [SerializeField] private ZoneReferenceProvider _zoneRefProvider;
        [SerializeField] private List<TransitionToZone> _transitions;
        [SerializeField] private int _startZoneIndex = 1;

        public override void InstallBindings() {

            Container.BindInstance(_worldRoot)
                    .AsSingle()
                    .NonLazy();

            Container.Bind<ZoneManager>()
                     .AsSingle()
                     .WithArguments(_zoneRefProvider, _transitions, _startZoneIndex, _worldRoot)
                     .NonLazy();
        }
    }
}