using R3;
using System.Collections.Generic;
using UnityEngine;
using ShopSystem.Storages;

namespace ChestsSystem {

    public sealed class ChestsPopupViewModel : IChestsPopupViewModel {
        private readonly ChestSystem _chestSystem;
        private readonly MoneyStorage _moneyStorage;
        private readonly CompositeDisposable _disposables = new();

        public IDictionary<ChestType, ReactiveChest> Chests => _chestSystem.Chests;
        public ChestVisualProvider ChestModelProvider => _chestSystem.ChestModelProvider;
        public ReactiveCommand<ReactiveChest> TryOpenReactiveChest { get; }

        public ChestsPopupViewModel(ChestSystem chestSystem, MoneyStorage moneyStorage) {
            _chestSystem = chestSystem;
            _moneyStorage = moneyStorage;

            TryOpenReactiveChest = new ReactiveCommand<ReactiveChest>();
            TryOpenReactiveChest
                .Subscribe(chest => HandleChestOpen(chest))
                .AddTo(_disposables);
        }

        private void HandleChestOpen(ReactiveChest chest) {

            if (_chestSystem.TryOpenChest(chest.Type, out var reward) == false) {
                Debug.LogError("Chest is not ready yet!");
                return;
            }

            _moneyStorage.AddMoney((long)reward.SoftCurrency);
            Debug.Log($"<color=yellow> Chest opened! Reward:" +
                $" SoftCurrency [{reward.SoftCurrency}]</color>");

            Debug.Log($"<color=yellow> Chest opened! Reward:" +
                $" Resources [{reward.Resources[0].Id}, {reward.Resources[0].Amount}]</color>");

            HandleChestReset(chest);
        }

        private void HandleChestReset(ReactiveChest chest) {

            if (_chestSystem.TryResetChest(chest.Type) == false) {
                Debug.Log("Chest is not reseted!");
                return;
            }
        }

        public void Dispose() {

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();
        }
    }
}
