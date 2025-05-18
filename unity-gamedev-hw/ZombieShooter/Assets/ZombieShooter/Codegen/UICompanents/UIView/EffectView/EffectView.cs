using Atomic.Elements;
using AtomicFramework.Effects;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZombieShooter.UI {

    public sealed class EffectView : MonoBehaviour {
        public BaseEvent<EffectView> OnComplite;

        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Image _filledImage;
        [SerializeField] private TMP_Text _durationText;

        private EffectViewModel _viewModel;
        private Countdown _countdown;

        public EffectType Type => _viewModel.Type;

        public void Init(EffectViewModel viewModel) {
            OnComplite = new BaseEvent<EffectView>();

            _viewModel = viewModel;
            _countdown = _viewModel.Countdown;

            _countdown.OnCurrentTimeChanged += OnDurationChanged;
            _countdown.OnProgressChanged += OnProgressChanged;
            _countdown.OnEnded += OnEnded;
            _countdown.Start();
        }

        private void Update() {
            _countdown.Tick(Time.deltaTime);
        }

        private void OnDurationChanged(float value) {

            _durationText.text = value.ToString("0.0");
        }

        private void OnProgressChanged(float progress) {
            _filledImage.fillAmount = 1 - progress;
        }

        private void OnEnded() {
            OnComplite?.Invoke(this);
        }
    }
}
