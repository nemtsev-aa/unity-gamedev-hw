using R3;
using Units.View;
using System.Collections.Generic;

namespace UICompanents {

    public interface IUnitsCreationViewModel {
        public ReadOnlyReactiveProperty<List<UnitViewModel>> ViewModels { get; }
    }
}