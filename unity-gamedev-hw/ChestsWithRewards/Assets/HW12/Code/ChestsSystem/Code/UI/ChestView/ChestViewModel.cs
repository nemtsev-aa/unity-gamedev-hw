namespace ChestsSystem {

    public sealed class ChestViewModel : IChestViewModel {
        public ReactiveChest ReactiveChest { get; }
        public ChestVisual Visual { get; }
        
        public ChestViewModel(ReactiveChest reactiveChest, ChestVisual visial) {
            ReactiveChest = reactiveChest;
            Visual = visial;
        }
    }
}
