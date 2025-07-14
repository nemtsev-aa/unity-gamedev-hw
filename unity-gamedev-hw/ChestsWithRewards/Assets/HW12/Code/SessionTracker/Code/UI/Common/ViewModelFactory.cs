using System;
using UnityEngine;
using GameCycleSystem;

namespace SessionTrackerSystem {

    public sealed class ViewModelFactory {
        private readonly SessionTracker _tracker;
        private readonly GameCycle _gameCycle;

        public ViewModelFactory(SessionTracker tracker, GameCycle gameCycle) {
            _tracker = tracker ?? throw new ArgumentNullException(nameof(tracker));
            _gameCycle = gameCycle ?? throw new ArgumentNullException(nameof(gameCycle));
        }

        public bool TryGetViewModel<T>(out T viewModel) where T : class, IViewModel {
            viewModel = null;

            if (typeof(T) == typeof(ISessionHistoryPopupViewModel)) {
                viewModel = CreateSessionHistoryViewModel() as T;
                return true;
            }

            if (typeof(T) == typeof(ISessionDataViewModel)) {
                viewModel = CreateSessionDataViewModel() as T;
                return true;
            }

            if (typeof(T) == typeof(IPopupSelectorViewModel)) {
                viewModel = CreatePopupSelectorViewModel() as T;
                return true;
            }

            Debug.LogError($"No factory implementation for view model type: {typeof(T)}");
            return false;
        }

        private ISessionHistoryPopupViewModel CreateSessionHistoryViewModel() {
            return new SessionHistoryPopupViewModel(_tracker);
        }

        private ISessionDataViewModel CreateSessionDataViewModel() {
            return new SessionDataViewModel(_tracker.CurrentSession);
        }

        private IPopupSelectorViewModel CreatePopupSelectorViewModel() {
            return new PopupSelectorViewModel(_gameCycle);
        }
    }
}


