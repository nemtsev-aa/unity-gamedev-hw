using Atomic.Elements;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

namespace ZombieShooter.UI {

    public class PlayerInfoView : MonoBehaviour, IDisposable {
        [SerializeField] private TMP_Text _hitPointsLabel;
        [SerializeField] private TMP_Text _bulletAmountLabel;
        [SerializeField] private TMP_Text _enemyKillsLabel;
        [Space(10)]
        [SerializeField] private float _fillRate = 1f;
        [SerializeField] private float _scaleOffset = 1.2f;

        private PlayerInfoViewModel _viewModel;

        private ReactiveVariable<float> _hitPoints;
        private ReactiveVariable<int> _bulletAmount;
        private ReactiveVariable<int> _maxBulletAmount;
        private ReactiveVariable<int> _killEnemyCount;
        private Sequence _sequence;

        public void Init(PlayerInfoViewModel viewModel) {
            _viewModel = viewModel;

            CreateReactiveSubscrebes();
        }

        private void CreateReactiveSubscrebes() {
            _hitPoints = _viewModel.HitPoints;
            _bulletAmount = _viewModel.BulletAmount;
            _maxBulletAmount = _viewModel.MaxBulletAmount;
            _killEnemyCount = _viewModel.KillEnemyCount;

            _hitPoints.Subscribe(OnHitPointsChanged);
            _hitPointsLabel.text = $"{_hitPoints.Value}"; 

            _bulletAmount.Subscribe(OnBulletAmountChanged);
            OnBulletAmountChanged(_bulletAmount.Value);

            _maxBulletAmount.Subscribe(OnMaxBulletAmountChanged);
            OnMaxBulletAmountChanged(_maxBulletAmount.Value);

            _killEnemyCount.Subscribe(OnKillEnemyCountChanged);
            _enemyKillsLabel.text = $"{_killEnemyCount.Value}";
        }

        private void OnHitPointsChanged(float value) {
            AnimateText(_hitPointsLabel, int.Parse(_hitPointsLabel.text), value);
        }

        private void OnBulletAmountChanged(int value) {
            _bulletAmountLabel.text = $"{value}/{_maxBulletAmount.Value}";
        }

        private void OnMaxBulletAmountChanged(int value) {
            _bulletAmountLabel.text = $"{_bulletAmount.Value}/{value}";
        }

        private void OnKillEnemyCountChanged(int value) {
            AnimateText(_enemyKillsLabel, int.Parse(_enemyKillsLabel.text), value);
        }

        private void AnimateText(TMP_Text textLabel, float currentValue, float nextValue) {
            _sequence?.Kill();

            float animatedValue = currentValue;

            _sequence = DOTween.Sequence()
                .Append(textLabel.transform.DOScale(Vector3.one * _scaleOffset, _fillRate / 2))
                .Append(DOTween.To(() => animatedValue,
                                    x => {
                                        animatedValue = x;
                                        textLabel.text = Mathf.RoundToInt(x).ToString();
                                    }, nextValue, _fillRate))
                .Append(textLabel.transform.DOScale(Vector3.one, _fillRate / 2));
        }

        public void Dispose() {

            if (_hitPoints != null)
                _hitPoints.Unsubscribe(OnHitPointsChanged);

            if (_bulletAmount != null)
                _bulletAmount.Unsubscribe(OnBulletAmountChanged);

            if (_maxBulletAmount != null)
                _maxBulletAmount.Unsubscribe(OnMaxBulletAmountChanged);

            if (_killEnemyCount != null)
                _killEnemyCount.Unsubscribe(OnKillEnemyCountChanged);
        }
    }
}
