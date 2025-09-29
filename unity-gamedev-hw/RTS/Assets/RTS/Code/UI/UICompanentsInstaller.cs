using Units.View;
using UnityEngine;
using Zenject;

namespace UICompanents {

    public sealed class UICompanentsInstaller : MonoInstaller {
        [SerializeField] private UIManager _uIManager;
        [SerializeField] private UnitViewConfigs _unitViewConfigs;

        public override void InstallBindings() {

            Container.BindInstance(_unitViewConfigs)
                .AsSingle()
                .NonLazy();

            Container.BindInstance(_uIManager)
                .AsSingle()
                .NonLazy();
        }
    }
}