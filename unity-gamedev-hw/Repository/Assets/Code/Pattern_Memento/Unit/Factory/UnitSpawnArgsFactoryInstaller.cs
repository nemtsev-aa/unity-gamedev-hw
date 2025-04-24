using UnityEngine;
using Zenject;

namespace Unit_Spawn_System {

    public sealed class UnitSpawnArgsFactoryInstaller : MonoInstaller {
        [SerializeField] private UnitPrefabConfigs _prefabConfigs;
        [SerializeField] private Transform _gameField;

        public override void InstallBindings() {
            BindFactory();
        }

        private void BindFactory() {
            Container.Bind<UnitSpawnArgsFactory>()
                .AsSingle()
                .WithArguments(_prefabConfigs, _gameField)
                .NonLazy();
        }
    }
}
