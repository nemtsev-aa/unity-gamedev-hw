using SaveSystem;
using Cysharp.Threading.Tasks;

namespace ProgressService {

    public sealed class PlayerProgressLoader {
        private const string Key = "PlayerData";

        private readonly Logger _logger;
        private readonly ISaveManager _savesManager;
        private PlayerProgressData _playerData;
        private bool _isLoadComplete;

        private IStorageService CurrentSaveService => _savesManager.CurrentService;

        public PlayerProgressLoader(Logger logger, ISaveManager saveManager) {
            _logger = logger;
            _savesManager = saveManager;
        }

        public void SavePlayerProgress(PlayerProgressData playerData) =>
            _savesManager.Save(Key, playerData, OnLevelProgressSaved);

        public async UniTask<PlayerProgressData> LoadPlayerProgress() {
            var tcs = new UniTaskCompletionSource<PlayerProgressData>();

            CurrentSaveService.Load<PlayerProgressData>(Key, data => {
                
                if (data.Chapter != 0) {
                    _playerData = data;
                    tcs.TrySetResult(data);
                    _isLoadComplete = true;

                    int currentChapter = _playerData.Chapter;
                    _logger.Log($"CurrentPlayerProgress: Current Chapter - [{currentChapter}]");
                } else {
                     tcs.TrySetResult(null);
                }
            });

            return await tcs.Task;
        }

        private void OnLevelProgressSaved(bool status) {
            if (status == true)
                _logger.Log("Save complited");
            else
                _logger.Log("Save falled");
        }
    }
}
