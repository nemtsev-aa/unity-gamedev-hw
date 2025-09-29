using R3;
using Units.View;
using UnityEngine;
using Client.Components.Common;
using System.Collections.Generic;

namespace UICompanents {

    public sealed class UnitsCreationView : MonoBehaviour {
        public Observable<UnitTypes> CreationClicked => _creationClicked;

        [SerializeField] private RectTransform _viewsRoot;
        [SerializeField] private UnitView _prefab;

        private readonly CompositeDisposable _disposables = new();
        private Subject<UnitTypes> _creationClicked = new Subject<UnitTypes>();
        
        private IUnitsCreationViewModel _viewModel;
        private List<UnitView> _views;


        public void Init(IUnitsCreationViewModel viewModel) {
            _viewModel = viewModel;

            CreateViews();
        }

        private void CreateViews() {

            _views = new List<UnitView>();

            foreach (var iViewModel in _viewModel.ViewModels.CurrentValue) {
                var newView = Instantiate(_prefab, _viewsRoot);
                newView.Init(iViewModel);

                newView.ViewSelected
                    .Subscribe(OnViewSelected)
                    .AddTo(_disposables);

                _views.Add(newView);
            }
        }

        private void OnViewSelected(UnitView view) {
            _creationClicked.OnNext(view.ViewModel.Type);
        }
    }
}