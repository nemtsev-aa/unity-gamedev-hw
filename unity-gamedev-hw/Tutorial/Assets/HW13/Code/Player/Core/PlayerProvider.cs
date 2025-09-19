using System;
using ProgressService;
using InteractionService;
using Cysharp.Threading.Tasks;
using BehaviorTree.PlayerCompanents;
using Progress = ProgressService.Progress;

namespace BehaviorTree.PlayerCoreSubsystem {

    public sealed class PlayerProvider {
        public Player Player { get; private set; }
        public PlayerProgressData ProgressData { get; private set; }
        public MoveCompanent Mover { get; private set; }
        public InteractionHandler InteractionHandler { get; private set; }
        public FellerCompanent Feller { get; private set; }
        public InventoryCompanent Inventory { get; private set; }
        public CollectorCompanent Collector { get; private set; }

        public bool IsInitialized { get; private set; }

        private readonly Progress _progress;
        private readonly UniTaskCompletionSource<bool> _initializationTcs = new();

        public PlayerProvider(Player player,
                              Progress progress) {

            Player = player;
            _progress = progress;

            var core = Player.Core;
            Mover = core.Mover;
            Feller = core.Feller;
            Inventory = core.Inventory;
            Collector = core.Collector;
            InteractionHandler = core.InteractionHandler;
        }

        public async UniTask InitializeAsync() {

            if (IsInitialized == true)
                return;

            try {
                await LoadPlayerProgressDataAsync();

                Player.Init();

                IsInitialized = true;
                _initializationTcs.TrySetResult(true);
            }
            catch (Exception ex) {
                _initializationTcs.TrySetException(ex);
            }
        }

        public void ResetProgress() => _progress.ResetProgress();

        public void MarkDataChanged() => _progress.MarkDataChanged();

        private async UniTask LoadPlayerProgressDataAsync() {
            await _progress.LoadProgress();

            int timeout = 100;
            while (_progress.ProgressData == null && timeout > 0) {
                await UniTask.Yield();
                timeout--;
            }

            if (_progress.ProgressData == null)
                throw new Exception("Failed to load player progress data");

            ProgressData = _progress.ProgressData;
        }
    }
}