using R3;
using UnityEngine;
using ShopSystem.Helpers;
using ShopSystem.Storages;
using ShopSystem.Product.Data;

namespace ShopSystem.Product.UI {

    public sealed class SaleProductViewModel : ISaleProductViewModel {
        private readonly ProductInfo _info;
        private readonly IBuyHandler _buyer;

        private readonly ReactiveProperty<bool> _canBuy = new();
        private readonly CompositeDisposable _compositeDisposable = new();

        public string Title { get; private set; }
        public string Description { get; private set; }
        public Sprite Icon { get; private set; }
        public string Price { get; private set; }

        public ReactiveCommand BuyCommand { get; private set; }
        public Observable<bool> CanBuy => _canBuy;

        public SaleProductViewModel(ProductInfo info, IBuyHandler buyer, MoneyStorage moneyStorage) {
            _info = info;
            _buyer = buyer;

            UpdateCompanents();
            CreteReactiveSubscribes(moneyStorage);
        }

        ~SaleProductViewModel() {
            Dispose();
        }

        public void Buy() =>
            _buyer.Buy(_info);

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

            BuyCommand = new ReactiveCommand(CanBuy, false);
            BuyCommand.Subscribe(OnBuyCommand).AddTo(_compositeDisposable);
        }

        private void OnBuyCommand(Unit _) => Buy();

        private void OnMoneyChange(long money) {
            _canBuy.Value = (money >= _info.MoneyPrice);

            //Debug.Log($"Current Money Value: {money}");
        }

        public void Dispose() {
            if (_compositeDisposable.IsDisposed == false)
                _compositeDisposable.Dispose();
        }
    }
}
