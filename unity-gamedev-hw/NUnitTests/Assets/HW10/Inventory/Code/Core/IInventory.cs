using System;
using System.Collections.Generic;

namespace InventorySystem.Core {

    public interface IInventory {
        event Action<InventoryItem> ItemAdded;
        event Action<InventoryItem> ItemRemoved;
        IReadOnlyList<InventoryItem> Items { get; }
        bool TryAddItem(InventoryItem item);
        bool TryRemoveItem(InventoryItem item);
        bool TryFindItem(string id, out InventoryItem item);
    }
}

