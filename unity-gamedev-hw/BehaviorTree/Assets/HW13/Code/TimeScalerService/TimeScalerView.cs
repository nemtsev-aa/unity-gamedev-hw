using R3;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace TimeScalerService {

    public sealed class TimeScalerView : MonoBehaviour, ITimeScalerView {
        public ReadOnlyReactiveProperty<float> TimeScaleValue => _timeScaleValue;

        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _labelText;

        private ReactiveProperty<float> _timeScaleValue = new(1f);

        public void Init() {
            _slider.onValueChanged.AddListener(OnScrollerValueChange);
        }

        public void SetScrollValue(float value) {
            _slider.value = value;
        }

        private void OnScrollerValueChange(float value) {
            _timeScaleValue.Value = value;
            _labelText.text = $"{value}"; 
        }

        public void Dispose() {
            _slider.onValueChanged.RemoveListener(OnScrollerValueChange);
        }

        private void OnValidate() {

            if (_slider == null)
                throw new ArgumentNullException($"TimeScalerView: Scroller not found!");

            if (_labelText == null)
                throw new ArgumentNullException($"TimeScalerView: TMP_Text not found!");
        }
    }
}