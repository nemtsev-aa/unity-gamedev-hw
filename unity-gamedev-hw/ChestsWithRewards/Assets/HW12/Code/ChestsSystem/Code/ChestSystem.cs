using GameCycleSystem;
using R3;
using SessionTrackerSystem;
using System;
using System.Collections.Generic;

namespace ChestsSystem {

    public sealed class ChestSystem : IGameInitializeListener,
                                      IGameStartListener,
                                      IDisposable {

        private readonly SessionTracker _sessionTracker;
        private readonly ChestSystemConfig _config;
        private readonly CompositeDisposable _disposables = new();
        private Dictionary<ChestType, ReactiveChest> _chests;
        private ReadOnlyReactiveProperty<TimeSpan> _currentSessionDuration;

        public IDictionary<ChestType, ReactiveChest> Chests => _chests;
        public ChestVisualProvider ChestModelProvider => _config.ChestVisualProvider;

        public ChestSystem(ChestSystemConfig config, SessionTracker sessionTracker) {
            _config = config;
            _sessionTracker = sessionTracker;
        }

        void IGameInitializeListener.OnInitializeGame() =>
            LoadChestsData();

        void IGameStartListener.OnStartGame() {
            _currentSessionDuration = _sessionTracker.CurrentSession.Duration;

            _currentSessionDuration
                .Subscribe(UpdateRemainingTimers)
                .AddTo(_disposables);

            StartAllChestTimers();
        }

        public bool TryOpenChest(ChestType type, out ChestReward reward) {
            
            if (_chests.TryGetValue(type, out var chest) == true)
                return chest.TryOpen(out reward, _sessionTracker.CurrentSessionDuration);

            reward = null;
            return false;
        }

        public bool TryResetChest(ChestType type) {

            if (_chests.TryGetValue(type, out var chest) == true)
                return chest.TryReset();

            return false;
        }

        private void LoadChestsData() {
            _chests = new Dictionary<ChestType, ReactiveChest>();
            var defaultData = _config.ChestDatas;

            for (int i = 0; i < defaultData.Count; i++) {
                ChestData data = defaultData[i];
                ChestReward reward = _config.ChestRewards[i];

                _chests.Add(data.Type, new ReactiveChest(data, reward));
            }
        }

        private void StartAllChestTimers() {

            DateTime startDateTime = DateTime.Now;

            foreach (var iChest in _chests.Values) {
                iChest.StartTimer(startDateTime);
            }
        }

        private void UpdateRemainingTimers(TimeSpan duration) {

            foreach (var iChest in _chests.Values) {

                if (iChest.IsReadyToOpen.CurrentValue == false)
                    iChest.UpdateRemainingTime(duration);
            }
        }

        public void Dispose() {

            foreach (var chest in _chests.Values)
                chest.Dispose();

            _disposables.Dispose();
        }
    }
}
