
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;


namespace UI.Components.DamagePopupSystem {

    public sealed class DamagePopupView : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private CanvasGroup _canvasGroup;

        [Header("Animation Settings")]
        [SerializeField] private float _moveDistance = 100f;
        [SerializeField] private float _duration = 1f;
        [SerializeField] private float _fadeInDuration = 0.2f;
        [SerializeField] private float _fadeOutDuration = 0.5f;

        [Header("Colors")]
        [SerializeField] private Color _damageColor = Color.red;
        [SerializeField] private Color _healColor = Color.green;

        private RectTransform _rectTransform;
        private Transform _originalParent;
        private Vector3 _originalPosition;
        private Action<DamagePopupView> _onAnimationComplete;
        private int _currentHealValue;
        private int _currentDamageValue;

        private void Awake() {
            _rectTransform = GetComponent<RectTransform>();
            _originalParent = _rectTransform.parent;
            _originalPosition = _rectTransform.anchoredPosition;
            _canvasGroup.alpha = 0f;
        }

        public void ShowDamage(int amount, HeroView heroView, Action<DamagePopupView> onComplete) {
            _onAnimationComplete = onComplete;
            _currentDamageValue = amount;
            ShowPopup($"-{_currentDamageValue}", _damageColor, heroView);
        }

        public void ShowHeal(int amount, HeroView heroView, Action<DamagePopupView> onComplete) {
            _onAnimationComplete = onComplete;
            _currentHealValue = amount;
            ShowPopup($"+{amount}", _healColor, heroView);
        }

        public void ChangeTextValue(int offset, bool heal) {

            if (heal == false) {
                _currentDamageValue += offset;
                _text.text = $"-{_currentDamageValue}";
                return;
            }

            _currentHealValue += offset;
            _text.text = $"+{_currentHealValue}";
        }

        private void ShowPopup(string text, Color color, HeroView view) {
            _text.text = text;
            _text.color = color;

            _rectTransform.SetParent(view.transform);
            _rectTransform.localPosition = Vector3.zero;
            _canvasGroup.alpha = 0f;

            AnimatePopup();
        }

        private void AnimatePopup() {
            _rectTransform.DOKill();
            _canvasGroup.DOKill();

            var sequence = DOTween.Sequence();

           sequence.Append(_canvasGroup.DOFade(1f, _fadeInDuration));
            sequence.Join(_rectTransform.DOAnchorPosY(_originalPosition.y + _moveDistance, _duration)
                .SetEase(Ease.OutBack));

            sequence.AppendInterval(_duration * 0.5f);
            sequence.Append(_canvasGroup.DOFade(0f, _fadeOutDuration)
                .SetEase(Ease.InQuad));

            sequence.OnComplete(() => {
                _onAnimationComplete?.Invoke(this);
                _onAnimationComplete = null;
            });

            sequence.Play();
        }

        public void Reset() {
            _rectTransform.DOKill();
            _canvasGroup.DOKill();
            _canvasGroup.alpha = 0f;

            _rectTransform.SetParent(_originalParent);
            _rectTransform.position = Vector3.zero;
            _onAnimationComplete = null;
        }

        private void OnDestroy() {
            _rectTransform.DOKill();
            _canvasGroup.DOKill();
        }
    }
}

