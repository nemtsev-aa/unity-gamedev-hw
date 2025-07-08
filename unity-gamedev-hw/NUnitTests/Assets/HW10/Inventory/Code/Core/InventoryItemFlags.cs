using System;

namespace InventorySystem.Core {

    [Flags]
    public enum InventoryItemFlags {
        NONE = 0,
        STACKABLE = 1,
        CONSUMABLE = 2,
        EQUPPABLE = 4,
        EFFECTIBLE = 8
    }
}

