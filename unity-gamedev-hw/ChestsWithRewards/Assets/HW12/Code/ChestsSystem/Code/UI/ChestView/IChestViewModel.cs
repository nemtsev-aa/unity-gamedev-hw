namespace ChestsSystem {
    public interface IChestViewModel {
        ReactiveChest ReactiveChest { get; }
        ChestVisual Visual { get; }
    }
}
