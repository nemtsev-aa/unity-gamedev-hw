using R3;

namespace SaveLoadSystem {

    namespace Core {

        public class SaveLoadManager {
            private ISaveLoader[] _saveLoaders;
            private GameRepository _gameRepository;
            private IContext _gameContext;

            private readonly Subject<Unit> _stateSaved = new Subject<Unit>();
            private readonly Subject<Unit> _stateLoaded = new Subject<Unit>();

            public SaveLoadManager(ISaveLoader[] saveLoaders, GameRepository gameRepository) {
                _saveLoaders = saveLoaders;
                _gameRepository = gameRepository;
            }

            public Observable<Unit> StateSaved => _stateSaved;
            public Observable<Unit> StateLoaded => _stateLoaded;

            public void SetContext(IContext gameContext) {
                _gameContext = gameContext;
            }

            public void SaveGame() {

                for (int i = 0; i < _saveLoaders.Length; i++) {
                    ISaveLoader saveLoader = _saveLoaders[i];
                    saveLoader.SaveGame(_gameContext, _gameRepository);
                }

                _gameRepository.SaveState();

                _stateSaved.OnNext(Unit.Default);
            }

            public void LoadGame() {
                _gameRepository.LoadState();

                for (int i = 0; i < _saveLoaders.Length; i++) {
                    ISaveLoader saveLoader = _saveLoaders[i];
                    saveLoader.LoadGame(_gameContext, _gameRepository);
                }

                _stateLoaded.OnNext(Unit.Default);
            }
        }
    }
}



