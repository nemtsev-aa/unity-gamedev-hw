using InventorySystem.Core;

namespace InventorySystem.EffectObservers {
    
    public interface IInventoryEffectObserver {
        void OnItemAdded(InventoryItem item);
        void OnItemRemoved(InventoryItem item);
    }
}

