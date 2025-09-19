using UnityEngine;

namespace Currencies.UI {

    public sealed class CurrencyProvider : MonoBehaviour {
        [SerializeField] private CurrencyView _moneyView;
        [SerializeField] private CurrencyView _gemView;

        public CurrencyView GemView => _gemView;
        public CurrencyView MoneyView => _moneyView;

        public void Show(bool status) =>
            gameObject.SetActive(status);
        
    }
}
