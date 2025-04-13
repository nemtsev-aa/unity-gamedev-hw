using Zenject;
using UnityEngine;

namespace PresentationModel {
    public sealed class CharacterPopupInstaller : MonoInstaller {
        [SerializeField] private DataConfigs _dataConfigs;

        public override void InstallBindings() {
            Container.BindInstance(_dataConfigs).AsSingle();
            Container.Bind<DataConfigProvider>().AsSingle();
            Container.Bind<CharacterPopupViewModelsFactory>().AsSingle();
        }
    }
}


