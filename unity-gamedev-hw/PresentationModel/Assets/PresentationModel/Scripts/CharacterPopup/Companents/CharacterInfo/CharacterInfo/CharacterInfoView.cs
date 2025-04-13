using R3;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PresentationModel {
    public sealed class CharacterInfoView : UIView, IDisposable {
        [SerializeField] private RectTransform _characterStatViewParent;
        [SerializeField] private CharacterStatView _characterStatViewPrefab;

        private readonly CompositeDisposable _disposables = new();
        private readonly List<CharacterStatView> _statViews = new();

        private ICharacterInfoViewModel _characterInfoViewModel;

        public override void Init(IViewModel viewModel) {
            if (viewModel is not ICharacterInfoViewModel characterInfoViewModel)
                throw new ArgumentNullException($"Invalid ViewModel: {viewModel}");
            
            _characterInfoViewModel = characterInfoViewModel;
            
            CreateReactiveSubscribes();
            CreateAllStatViews();
        }

        public override void UpdateCompanents() {

            if (_characterInfoViewModel.StatViewModels.Count == 0)
                return;

            foreach (var viewModel in _characterInfoViewModel.StatViewModels) {
                var existingView = _statViews.FirstOrDefault(v => v.Name == viewModel.Name);

                if (existingView != null) {
                    existingView.UpdateCompanents();
                    continue;
                }

                var newView = Instantiate(_characterStatViewPrefab, _characterStatViewParent);
                newView.Init(viewModel);
                _statViews.Add(newView);
            }
        }

        private void CreateReactiveSubscribes() {
            _characterInfoViewModel.OnAnyStatChanged
                .Subscribe(_ => UpdateStatViews())
                .AddTo(_disposables);
        }

        private void CreateAllStatViews() {
            ClearStatViews();

            foreach (var statViewModel in _characterInfoViewModel.StatViewModels) {
                var view = Instantiate(_characterStatViewPrefab, _characterStatViewParent);
                view.Init(statViewModel);
                
                _statViews.Add(view);
            }
        }

        private void UpdateStatViews() {

            var currentViewModels = _characterInfoViewModel.StatViewModels;

            for (int i = _statViews.Count - 1; i >= 0; i--) {
                var view = _statViews[i];

                if (!currentViewModels.Any(vm => vm.Name == view.Name)) {
                    view.Dispose();
                    Destroy(view.gameObject);
                    _statViews.RemoveAt(i);
                }
            }

            UpdateCompanents();
        }

        private void ClearStatViews() {
            foreach (var view in _statViews) {
                
                if (view != null) {
                    view.Dispose();
                    Destroy(view.gameObject);
                }
            }

            _statViews.Clear();
        }

        public void Dispose() {
            
            if (_disposables.IsDisposed == false) {
                _disposables.Dispose();
                ClearStatViews();
            }
        }
    }
}