using System;
using Cysharp.Threading.Tasks;

namespace ProgressService {

    [Serializable]
    public sealed class Progress {
        public event Action DataChanged;
        public PlayerProgressData ProgressData { get; private set; }

        private readonly PlayerProgressLoader _loader;
        private readonly Logger _logger;
        private bool _isSaving;

        public Progress(PlayerProgressLoader loader, Logger logger) {
            _loader = loader;
            _logger = logger;
        }

        public async UniTask LoadProgress() {
            ProgressData = await _loader.LoadPlayerProgress();
            
            if (ProgressData == null) {
                ProgressData ??= CreateDefaultProgress();
                _logger.Log($"DefaultPlayerProgress loading success");
            }
 
            DataChanged?.Invoke();
        }

        public void SaveProgress() {
            if (_isSaving == true)
                return;

            _isSaving = true;
            _loader.SavePlayerProgress(ProgressData);

            _isSaving = false;
        }

        public void ResetProgress() {
            ProgressData = CreateDefaultProgress();
            _logger.Log($"DefaultPlayerProgress loading success");
            
            DataChanged?.Invoke();
            SaveProgress();
        }

        public void MarkDataChanged() {
            DataChanged?.Invoke();
            SaveProgress();
        }

        private PlayerProgressData CreateDefaultProgress() {
            return new PlayerProgressData(0, 0, 0, 0, 0);
        }
    }
}



