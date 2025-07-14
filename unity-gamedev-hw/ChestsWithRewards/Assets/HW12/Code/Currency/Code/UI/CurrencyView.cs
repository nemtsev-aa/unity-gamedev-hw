using TMPro;
using UnityEngine;
using DG.Tweening;

namespace Currencies.UI {

    public sealed class CurrencyView : MonoBehaviour {
        [SerializeField] private TMP_Text _currencyText;
        [SerializeField] private ScaleTweenParams _startScale;
        [SerializeField] private ScaleTweenParams _endScale;
        [SerializeField] private float _duration;

        private Sequence _sequence;

        public float Duration => _duration;

        public void UpdateCurrency(string currency) {
            _currencyText.text = currency;
        }

        public Sequence AnimateStartText() {
            return DOTween
                .Sequence()
                .Append(_currencyText.transform.DOScale(_startScale.Scale, _startScale.Duration));
        }

        public Sequence AnimateEndText() {
            return DOTween
                .Sequence()
                .Append(_currencyText.transform.DOScale(_endScale.Scale, _endScale.Duration));
        }
    }
}
