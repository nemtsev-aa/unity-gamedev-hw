using R3;
using Units.View;
using System.Collections.Generic;

namespace UICompanents {

    public sealed class UnitsCreationViewModel : IUnitsCreationViewModel {
        public ReadOnlyReactiveProperty<List<UnitViewModel>> ViewModels => _viewModels;
        
        private ReactiveProperty<List<UnitViewModel>> _viewModels = new ();

        public UnitsCreationViewModel(List<UnitViewConfig> configs) {
            _viewModels.Value = new List<UnitViewModel>();

            for (int i = 0; i < configs.Count; i++) {
                var config = configs[i];
                var viewModel = new UnitViewModel(config);

                _viewModels.Value.Add(viewModel);
            }
        }
    }
}