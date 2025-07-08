namespace InventorySystem.ItemComponents {
    
    public interface IPassiveEffectItemComponent : IItemComponent {
        string Type { get; }
        int Value { get; }
    }
}

