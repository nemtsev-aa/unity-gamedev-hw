using R3;
using System;
using UnityEngine;

namespace ChestsSystem {

    public sealed class ReactiveChest : IDisposable {
        public readonly ChestType Type;
        public readonly ChestReward ChestReward;

        public ReadOnlyReactiveProperty<bool> IsReadyToOpen => _isReadyToOpen;
        public ReadOnlyReactiveProperty<TimeSpan> RemainingTime => _remainingTime;
        public Subject<Unit> Opened = new Subject<Unit>();

        private readonly TimeSpan _cooldown;
        private readonly CompositeDisposable _disposables = new();

        private ReactiveProperty<bool> _isReadyToOpen = new ReactiveProperty<bool>();
        private ReactiveProperty<TimeSpan> _remainingTime = new ReactiveProperty<TimeSpan>();
        private DateTime _startTime = DateTime.MinValue;
        private TimeSpan _lastOpenTime;

        public ReactiveChest(ChestData data, ChestReward chestReward) {
            Type = data.Type;
            _cooldown = data.Cooldown.GetTimeSpan();
            _remainingTime = new ReactiveProperty<TimeSpan>(_cooldown);
            _lastOpenTime = TimeSpan.Zero;

            ChestReward = chestReward;
        }

        public void StartTimer(DateTime startTime) {
            _startTime = startTime;
        }

        public void UpdateRemainingTime(TimeSpan time) {

            if (_startTime == DateTime.MinValue || time == TimeSpan.Zero)
                return;

            _remainingTime.Value = _cooldown - (time - _lastOpenTime);

            CheckIfReady();
        }

        public bool TryOpen(out ChestReward reward, TimeSpan sessionDuration) {
            if (CheckIfReady() == false) {
                reward = null;
                return false;
            }

            reward = ChestReward;
            _lastOpenTime = sessionDuration;
            Opened.OnNext(default);

            return true;
        }

        public bool TryReset() {
            _remainingTime.Value = _cooldown;
            _startTime = DateTime.Now;
            _isReadyToOpen.Value = false;

            return true;
        }

        private bool CheckIfReady() {

            if (_remainingTime.Value <= TimeSpan.Zero) {
                _remainingTime.Value = TimeSpan.Zero;

                _isReadyToOpen.Value = true;
                return true;
            }

            _isReadyToOpen.Value = false;
            return false;
        }

        public void Dispose() {

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();
        }
    }
}
