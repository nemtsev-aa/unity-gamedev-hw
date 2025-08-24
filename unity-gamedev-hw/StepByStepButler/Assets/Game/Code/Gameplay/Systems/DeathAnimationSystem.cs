using Cysharp.Threading.Tasks;
using DG.Tweening;
using EventBusService;
using StepByStepButler.Gameplay.Heroes;
using System.Collections.Generic;
using System.Threading;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace StepByStepButler.Gameplay.Systems {

    public sealed class DeathAnimationSystem : IGameSystem {
        private readonly DeathAnimationSettings _settings;
        private readonly Dictionary<HeroType, GameObject> EffectsDictionary;

        private readonly IEventBus _eventBus;
        private Vector3 _originalScale;
        private Color _originalColor;
        private Image _iconImage;

        public DeathAnimationSystem(IEventBus eventBus,
                                    DeathAnimationSettings settings) {

            _eventBus = eventBus;
            _settings = settings;

            EffectsDictionary = _settings.GetEffectsDictionary();
        }

        public void OnInitializeGame() {
            _eventBus.Subscribe<HeroDeathAnimationEvent>(PlayDeathAnimation);
        }

        public void OnFinishGame() {
            _eventBus.Unsubscribe<HeroDeathAnimationEvent>(PlayDeathAnimation);
        }

        public void OnRestartGame() { }

        public async void PlayDeathAnimation(HeroDeathAnimationEvent @event) {
            try {
                _eventBus.Publish(new HeroDeathEvent(@event.HeroId));
                await AnimateDeath(@event.HeroView, @event.HeroType);
            }
            finally {
                _eventBus.Publish(new DeathAnimationCompleteEvent(@event.HeroId));
            }
        }

        private async UniTask AnimateDeath(HeroView heroView, HeroType heroType) {
            var cts = new CancellationTokenSource();
            var linkedToken = CancellationTokenSource.CreateLinkedTokenSource(
                cts.Token,
                heroView.GetCancellationTokenOnDestroy()).Token;

            GetOriginalParameters(heroView);

            var cancellationToken = heroView.GetCancellationTokenOnDestroy();
            var shakeTween = heroView.transform
                .DOShakePosition(
                    duration: _settings.ShakeDuration,
                    strength: _settings.ShakeStrength,
                    vibrato: _settings.ShakeVibrato,
                    randomness: 90f,
                    snapping: false,
                    fadeOut: true)
                .AsyncWaitForCompletion()
                .AsUniTask();

            _iconImage.sprite = _settings.DeathIcon;
            var fadeTween = _iconImage.DOFade(0.2f, _settings.FadeDuration)
                                      .SetEase(Ease.InQuad)
                                      .AsyncWaitForCompletion()
                                      .AsUniTask();

            var scaleTween = heroView.transform
                                     .DOScale(
                                        heroView.transform.localScale * _settings.ScaleDownMultiplier,
                                        _settings.FadeDuration)
                                     .SetEase(Ease.InBack)
                                     .AsyncWaitForCompletion()
                                     .AsUniTask();

            var effectTask = PlayDeathEffect(heroView, heroType, linkedToken);

            await UniTask.WaitForSeconds(_settings.FadeDuration);

            await UniTask.WhenAll(
                shakeTween,
                fadeTween,
                scaleTween,
                effectTask
            );

            heroView.gameObject.SetActive(false);
            ResetAppearance(heroView);
        }

        private void GetOriginalParameters(HeroView view) {
            _originalScale = view.transform.localScale;

            if (TryGetIconImage(view, out var iconImage) == false)
                return;

            _iconImage = iconImage;
            _originalColor = iconImage.color;
        }

        public void ResetAppearance(HeroView view) {
            view.transform.localScale = _originalScale;

            if (_iconImage != null)
                _iconImage.color = _originalColor;
        }

        private bool TryGetIconImage(HeroView view, out Image iconImage) {

            var parent = view.GetComponent<RectTransform>();
            var iconImageTransform = FindDeepChild(parent, "Icon");

            if (iconImageTransform != null) {

                if (iconImageTransform.TryGetComponent(out Image icon) == true) {
                    iconImage = icon;
                    return true;
                }
            }

            iconImage = null;
            return false;
        }

        private RectTransform FindDeepChild(RectTransform parent, string name) {
            foreach (RectTransform child in parent) {

                if (child.name == name)
                    return child;
            }

            foreach (RectTransform child in parent) {
                var result = FindDeepChild(child, name);

                if (result != null)
                    return result;
            }

            return null;
        }

        private async UniTask PlayDeathEffect(HeroView heroView,
                                              HeroType heroType,
                                              CancellationToken cancellationToken) {

            if (EffectsDictionary.TryGetValue(heroType, out var effectPrefab)) {
                var effect = GameObject.Instantiate(effectPrefab,
                    heroView.transform.position,
                    Quaternion.identity);

                try {
                    var ps = effect.GetComponent<ParticleSystem>();

                    if (ps != null) {
                        await ps.WaitForCompletionAsync(cancellationToken);
                    } else {
                        await UniTask.Delay(2000, cancellationToken: cancellationToken);
                    }
                }
                finally {

                    if (effect != null)
                        GameObject.Destroy(effect);
                }
            }
        }
    }
}