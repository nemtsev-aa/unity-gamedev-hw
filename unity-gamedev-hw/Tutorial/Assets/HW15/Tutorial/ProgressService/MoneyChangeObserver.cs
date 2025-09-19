using R3;
using UnityEngine;
using ShopSystem.Storages;

namespace ProgressService {
    public sealed class MoneyChangeObserver {
        private readonly MoneyStorage _moneyStorage;
        private PlayerProgressData _progressData;
        private CompositeDisposable _disposables;

        public MoneyChangeObserver(MoneyStorage moneyStorage) {
            _moneyStorage = moneyStorage;
        }

        ~MoneyChangeObserver() {

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();
        }

        public void Init(PlayerProgressData data) {
            _disposables = new();
            _progressData = data;

            _moneyStorage.SetMoney(data.Coins);

            CreateReactiveSubscribes();
        }

        private void CreateReactiveSubscribes() {

            _moneyStorage.Money
                .Skip(1)
                .Subscribe(OnMoneyChanged)
                .AddTo(_disposables);
        }

        private void OnMoneyChanged(long money) {

            if (_progressData.Coins != money)
                _progressData.Coins = Mathf.RoundToInt(money);
        }
    }
}