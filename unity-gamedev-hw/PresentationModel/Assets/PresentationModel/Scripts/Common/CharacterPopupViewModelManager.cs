using R3;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace PresentationModel {
    public sealed class CharacterPopupViewModelManager : MonoBehaviour {
        [SerializeField] private List<CharacterPopupConfig> _popupConfigs;
        [Space(10)]
        [SerializeField] private int _popupConfigIndex = 1;
        [Space(10)]
        [SerializeField] private bool _loadConfigFromInspector;

        private readonly CompositeDisposable _compositeDisposable = new ();
        private CharacterPopupViewModelsFactory _factory;
        private CharacterPopupPresenterView _presenterView;
        private ReactiveProperty<CharacterPopupViewModel> _viewModel = new ();

        public ReadOnlyReactiveProperty<CharacterPopupViewModel> ViewModel => _viewModel;

        [Inject]
        public void Construct(CharacterPopupViewModelsFactory factory) {
            _factory = factory;
        }

        private void OnValidate() {
            _popupConfigIndex = Mathf.Clamp(_popupConfigIndex, 0, _popupConfigs.Count - 1);
        }

        public void Init(CharacterPopupPresenterView presenterView) {
            _presenterView = presenterView;

            CreateReactiveSubscribes();
            CreateViewModel();
        }

        private void CreateReactiveSubscribes() {
            _presenterView.OnInitClickObservable
                .Subscribe(OnInitClick)
                .AddTo(_compositeDisposable);
        }

        private void CreateViewModel() {
            _viewModel.Value = GetViewModel();

            if (_viewModel.Value == null)
                throw new ArgumentNullException($"CharacterPopupViewModel is empty!");
        }

        private CharacterPopupViewModel GetViewModel() {
            
            if (_loadConfigFromInspector) {

                if (_popupConfigs.Count == 0)
                    throw new ArgumentNullException($"CharacterPopupConfigs is empty!");

                return _factory.Create(_popupConfigs[_popupConfigIndex]);
            }

            return _factory.Create();
        }

        private void OnInitClick(Unit unit) =>
            CreateViewModel();

        public void Dispose() {
            if (_compositeDisposable.IsDisposed == false) {
                _compositeDisposable.Dispose();
            }
        }
    }
}


