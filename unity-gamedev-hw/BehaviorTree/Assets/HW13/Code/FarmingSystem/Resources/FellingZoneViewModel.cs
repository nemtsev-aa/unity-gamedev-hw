using ObservableCollections;
using R3;

namespace FarmingSystem {
    public sealed class FellingZoneViewModel : IFellingZoneViewModel {
        public ReadOnlyReactiveProperty<int> ReactiveTreesAmount { get; private set; }
        public ReadOnlyReactiveProperty<int> ReactiveLootAmount { get; private set; }

        private readonly FellingZone _fellingZone;

        public FellingZoneViewModel(FellingZone fellingZone) {
            _fellingZone = fellingZone;

            ReactiveTreesAmount = _fellingZone.ReactiveTreesAmount;
            ReactiveLootAmount = _fellingZone.ReactiveLootAmount;
        }
    }
}
