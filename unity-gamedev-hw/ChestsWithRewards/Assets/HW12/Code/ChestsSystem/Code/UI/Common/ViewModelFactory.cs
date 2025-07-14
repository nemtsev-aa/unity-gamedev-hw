using SessionTrackerSystem;
using ShopSystem.Storages;
using System;
using UnityEngine;

namespace ChestsSystem {

    public sealed class ViewModelFactory {
        private readonly ChestSystem _chestSystem;
        private readonly MoneyStorage _moneyStorage;

        public ViewModelFactory(ChestSystem chestSystem, MoneyStorage moneyStorage) {
            _chestSystem = chestSystem ?? throw new ArgumentNullException(nameof(chestSystem));
            _moneyStorage = moneyStorage ?? throw new ArgumentNullException(nameof(moneyStorage));
        }

        public bool TryGetViewModel<T>(out T viewModel) where T : class, IViewModel {
            viewModel = null;

            if (typeof(T) == typeof(IChestsPopupViewModel)) {
                viewModel = CreateChestsPopupViewModel() as T;
                return true;
            }

            Debug.LogError($"No factory implementation for view model type: {typeof(T)}");
            return false;
        }

        private IChestsPopupViewModel CreateChestsPopupViewModel() {
            return new ChestsPopupViewModel(_chestSystem, _moneyStorage);
        }
    }
}
