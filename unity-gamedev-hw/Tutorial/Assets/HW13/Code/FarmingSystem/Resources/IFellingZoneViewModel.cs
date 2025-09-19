using R3;

namespace FarmingSystem {
    public interface IFellingZoneViewModel {
        public ReadOnlyReactiveProperty<int> ReactiveTreesAmount { get; }
        public ReadOnlyReactiveProperty<int> ReactiveLootAmount { get; }
    }
}
