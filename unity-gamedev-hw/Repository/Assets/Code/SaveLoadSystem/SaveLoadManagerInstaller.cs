using Pattern_Memento;
using SaveLoadSystem.Core;
using UnityEngine;
using Zenject;

namespace SaveLoadSystem {

    public class SaveLoadManagerInstaller : MonoInstaller {
        [SerializeField] private GameStateSaverType _saverType;
        [SerializeField] private SaveLoadPopup _saveLoadPopup;
        
        private EncryptionSystem _encryptionSystem;

        public override void InstallBindings() {
            JsonProjectSettings.ApplyProjectSerializationSettings();

            BindGameRepository();
            BindZenjectContext();
            BindSaveLoadManager();
            BindSaveLoadPopup();
        }

        private void BindGameRepository() {

            Container.Bind<GameRepository>()
                .AsSingle()
                .WithArguments(GetSaverByType(_saverType))
                .NonLazy();
        }

        private void BindZenjectContext() {

            Container.Bind<ZenjectContext>()
                .AsSingle()
                .WithArguments(Container);
        }

        private void BindSaveLoadManager() {
            UnitSaveLoader unitMementoSaveLoader = new UnitSaveLoader();
            ResourceSaveLoader resourceMementoSaveLoader = new ResourceSaveLoader();

            ISaveLoader[] saveLoaders = { unitMementoSaveLoader, resourceMementoSaveLoader };

            Container.Bind<SaveLoadManager>()
                .AsSingle()
                .WithArguments(saveLoaders)
                .NonLazy();

            Container.Resolve<SaveLoadManager>()
                .SetContext(Container.Resolve<ZenjectContext>());
        }

        private void BindSaveLoadPopup() {
            Container.BindInstance(_saveLoadPopup)
                .AsSingle();
        }

        private IGameStateSaver GetSaverByType(GameStateSaverType saverType) {

            if (_encryptionSystem == null)
                _encryptionSystem = EncryptionSystem.CreateNew();

            switch (saverType) {
                case GameStateSaverType.BinaryToFile:
                    return new BinaryToFileStateSaver();

                case GameStateSaverType.JsonToFile:
                    return new JsonToFileGameStateSaver();

                case GameStateSaverType.JsonEncryptionToFile:
                    return new JsonEncryptionToFileGameStateSaver(_encryptionSystem);

                case GameStateSaverType.PlayerPrefs:
                    return new PlayerPrefsGameStateSaver();

                default:
                    return new PlayerPrefsGameStateSaver();
            }
        }
    }
}