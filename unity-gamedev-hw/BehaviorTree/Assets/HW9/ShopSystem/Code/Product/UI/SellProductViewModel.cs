using R3;
using UnityEngine;
using ShopSystem.Helpers;
using ShopSystem.Product.Data;
using ShopSystem.Storages;

namespace ShopSystem.Product.UI {

    public sealed class SellProductViewModel : ISellProductViewModel {
        private readonly ProductInfo _info;
        private readonly ProductSeller _seller;

        private readonly ReactiveProperty<bool> _canSell = new();
        private readonly CompositeDisposable _compositeDisposable = new();

        public string Title { get; private set; }
        public string Description { get; private set; }
        public Sprite Icon { get; private set; }
        public string Price { get; private set; }

        public Observable<bool> CanSell => _canSell;
        public ReactiveCommand SellCommand { get; private set; }
        public Subject<SellProductViewModel> Cleared { get; } = new();

        public SellProductViewModel(ProductInfo info, ProductSeller seller, MoneyStorage moneyStorage) {
            _info = info;
            _seller = seller;

            UpdateCompanents();
            CreteReactiveSubscribes(moneyStorage);
        }

        public void Sell(int amount = 1) {
            _seller.TrySellProduct(_info.ID, amount);
        }

        private void UpdateCompanents() {
            Title = _info.Title;
            Description = _info.Description;
            Icon = _info.Icon;
            Price = _info.MoneyPrice.ToString();
        }

        private void CreteReactiveSubscribes(MoneyStorage moneyStorage) {
            moneyStorage.Money
                .Subscribe(OnMoneyChange)
                .AddTo(_compositeDisposable);

            SellCommand = new ReactiveCommand(CanSell, false);
            SellCommand.Subscribe(OnSellCommand).AddTo(_compositeDisposable);
        }

        private void OnSellCommand(Unit _) {
            Sell();

            Cleared.OnNext(this);
        }

        private void OnMoneyChange(long money) {
            _canSell.Value = true;
        }

        public void Dispose() {

            if (_compositeDisposable.IsDisposed == false)
                _compositeDisposable.Dispose();
        }
    }
}
