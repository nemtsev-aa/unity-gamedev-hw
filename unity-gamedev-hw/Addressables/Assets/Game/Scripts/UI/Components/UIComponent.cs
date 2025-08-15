using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using UnityEngine;

namespace UI.Components {

    public abstract class UIComponent : MonoBehaviour {
        public bool IsActive { get; private set; }
        public bool IsInitialized { get; private set; }

        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeDuration = 0.3f;

        private CancellationTokenSource _animationCTS;

        protected virtual void Awake() {

            if (_canvasGroup == null)
                _canvasGroup = GetComponent<CanvasGroup>() ?? gameObject.AddComponent<CanvasGroup>();

            _canvasGroup.alpha = 0f;
            gameObject.SetActive(false);
            IsActive = false;
        }

        protected virtual void OnDestroy() {
            _animationCTS?.Cancel();
            _animationCTS?.Dispose();
        }

        public virtual void Initialize() {
            if (IsInitialized == true)
                return;

            OnInitialize();
            IsInitialized = true;
        }

        protected virtual void OnInitialize() { }

        public virtual async UniTask ShowAsync(bool animated = true) {
            
            if (IsActive == true)
                return;

            _animationCTS?.Cancel();
            _animationCTS = new CancellationTokenSource();

            IsActive = true;
            gameObject.SetActive(true);

            if (animated && _canvasGroup != null) {
                _canvasGroup.alpha = 0f;
                await _canvasGroup.DOFade(1f, _fadeDuration)
                    .SetEase(Ease.OutQuad)
                    .AsyncWaitForCompletion() 
                    .AsUniTask() 
                    .AttachExternalCancellation(_animationCTS.Token); 
            } else if (_canvasGroup != null) {
                _canvasGroup.alpha = 1f;
            }

            OnShown();
        }

        public virtual async UniTask HideAsync(bool animated = true) {
            
            if (IsActive == false)
                return;

            OnHiding();

            _animationCTS?.Cancel();
            _animationCTS = new CancellationTokenSource();

            if (animated && _canvasGroup != null) {
                await _canvasGroup.DOFade(0f, _fadeDuration)
                    .SetEase(Ease.InQuad)
                    .AsyncWaitForCompletion()
                    .AsUniTask()
                    .AttachExternalCancellation(cancellationToken: _animationCTS.Token);
            }

            IsActive = false;
            gameObject.SetActive(false);
            OnHidden();
        }

        protected virtual void OnShown() { }
        protected virtual void OnHiding() { }
        protected virtual void OnHidden() { }
    }
}
