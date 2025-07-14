using R3;
using System;
using DG.Tweening;
using Currencies.UI;

namespace ShopSystem.Storages {

    public sealed class MoneyPanelAdapter : IDisposable {
        private readonly CurrencyView _currencyView;
        private readonly CompositeDisposable _disposable = new ();

        private long _lastCurrency;
        private Sequence _sequence;

        public MoneyPanelAdapter(CurrencyView currencyView, MoneyStorage moneyStorage) {
            _currencyView = currencyView;
            
            moneyStorage.Money
                .Skip(1)
                .Subscribe(OnMoneyChanged);
            
            OnMoneyChanged(moneyStorage.Money.CurrentValue);
        }

        private void OnMoneyChanged(long money) {
            _sequence?.Kill();
            
            var tweenerCore = DOTween.To(() => _lastCurrency, Setter, money, _currencyView.Duration);
            
            _sequence = DOTween.Sequence()
                .Append(_currencyView.AnimateStartText())
                .Append(tweenerCore)
                .Append(_currencyView.AnimateEndText());
        }

        private void Setter(long value) {
            _currencyView.UpdateCurrency(value.ToString());
            _lastCurrency = value;
        }

        public void Dispose() {
            _sequence?.Kill();

            if (_disposable.IsDisposed == false)
                _disposable.Dispose();
        }
    }
}
