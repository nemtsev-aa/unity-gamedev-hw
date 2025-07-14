using System;
using Zenject;
using DG.Tweening;
using Currencies.UI;

namespace ShopSystem.Storages {
    
    public sealed class GemPanelAdapter : IInitializable, IDisposable {
        private readonly CurrencyView _currencyView;
        private readonly GemStorage _gemStorage;

        private long _lastCurrency;
        private Sequence _sequence;

        public GemPanelAdapter(CurrencyView currencyView, GemStorage gemStorage) {
            _currencyView = currencyView;
            _gemStorage = gemStorage;
        }

        public void Initialize() {
            _gemStorage.OnGemChanged += OnGemChanged;
            Setter(_gemStorage.Gem);
        }

        private void OnGemChanged(long gem) {
            _sequence?.Kill();
            var tweenerCore = DOTween.To(() => _lastCurrency, Setter, gem, _currencyView.Duration);
            
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
            _gemStorage.OnGemChanged -= OnGemChanged;
        }
    }
}
