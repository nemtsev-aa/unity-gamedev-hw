using R3;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
using UI.Components;

namespace CharactersSystem.Player.Skins {

    public sealed class PlayerCharacterSkinScreen : UIComponent, IDisposable {
        public Observable<int> SkinSelected => _skinSelected;

        [SerializeField] private RectTransform _skinViewParent;
        [SerializeField] private PlayerCharacterSkinView _skinViewPrefab;
        [SerializeField] private Button _applyButton;

        [Header("Animation Settings")]
        [Space, SerializeField] private float _animationDuration = 0.3f;
        [SerializeField] private float _delayBetweenItems = 0.1f;
        [SerializeField] private Vector2 _scaleValues = new Vector2(0.5f, 1f);
        [SerializeField] private Ease _easeType = Ease.OutBack;

        private Observable<Unit> ApplyButtonClicked => _applyButton.OnClickAsObservable();

        private Subject<int> _skinSelected;
        private CompositeDisposable _disposables;
        private IPlayerCharacterSkinScreenViewModel _viewModel;
        private List<PlayerCharacterSkinView> _views = new();
        private int _selectedSkinIndex;
        private CancellationTokenSource _animationCts;

        public void SetViewModel(IPlayerCharacterSkinScreenViewModel viewModel) {
            Reset();

            _viewModel = viewModel;
            _disposables = new();

            _skinSelected = new Subject<int>();
            CreateSkinViews();
            CreateReactiveSubscribes();

            OnShown();
        }

        protected override async void OnShown() {
            base.OnShown();
            await PlayShowAnimationAsync();
        }

        private async UniTask PlayShowAnimationAsync() {
            _animationCts?.Cancel();
            _animationCts = new CancellationTokenSource();

            try {

                foreach (var view in _views) {
                    view.transform.DOScale(_scaleValues.y, _animationDuration)
                        .SetEase(_easeType)
                        .From(_scaleValues.x);

                    await UniTask.Delay(TimeSpan.FromSeconds(_delayBetweenItems),
                        cancellationToken: _animationCts.Token);
                }
            }
            catch (OperationCanceledException) {
                // Анимация была отменена - это нормально
            }
        }

        public void Reset() {

            if (_views.Count == 0)
                return;

            foreach (var view in _views) {
                
                if (view != null)
                    Destroy(view.gameObject);
            }

            _views.Clear();
            _disposables?.Dispose();
            _disposables = null;
        }

        private void CreateSkinViews() {

            for (int i = 0; i < _viewModel.Skins.Count; i++) {
                var iSkin = _viewModel.Skins[i];
                var metaData = iSkin.Meta;

                if (CheckViews(metaData.Id) == true)
                    continue;

                var viewModel = new PlayerCharacterSkinViewModel(metaData.Id, metaData.Name, metaData.Icon);

                if (TryGetSkinView(viewModel, out var newView) == true) {

                    if (_views.Contains(newView) == false)
                        _views.Add(newView);
                }
            }

            PreparingforAnimation();
        }

        private void PreparingforAnimation() {

            if (_views.Count == 0)
                return;

            foreach (var view in _views) {
                view.transform.localScale = Vector3.zero;
            }
        }

        private bool CheckViews(int id) {
            var skinView = _views.FirstOrDefault(v => v.Id == id);

            if (skinView != null)
                return true;

            return false;
        }

        private bool TryGetSkinView(IPlayerCharacterSkinViewModel viewModel, out PlayerCharacterSkinView skinView) {

            try {
                var newView = Instantiate(_skinViewPrefab, _skinViewParent);

                newView.Init(viewModel);
                newView.Selected
                    .Subscribe(OnSelected)
                    .AddTo(_disposables);

                skinView = newView;
                return true;
            }
            catch (Exception e) {
                Debug.LogError($"Error in TryGetSkinView for skin {viewModel.Id}: {e.Message}");
                skinView = null;

                return false;
            }
        }

        private void OnSelected(int id) {

            if (_selectedSkinIndex != id)
                _selectedSkinIndex = id;
        }

        private void CreateReactiveSubscribes() {

            ApplyButtonClicked
                .Subscribe(OnApplyButtonClicked)
                .AddTo(_disposables);
        }

        private void OnApplyButtonClicked(Unit _) {
            _skinSelected.OnNext(_selectedSkinIndex);
        }

        protected override void OnHidden() {
            base.OnHidden();

            PreparingforAnimation();

            _animationCts?.Cancel();
            _animationCts?.Dispose();
            _animationCts = null;
        }

        public void Dispose() {

            if (_disposables.IsDisposed == false)
                _disposables.Dispose();

            _skinSelected?.Dispose();
            _disposables = null;
        }
    }
}
