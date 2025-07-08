using UnityEngine;

namespace InventorySystem.UI {
    
    public interface IInventoryItemPresentationModel {
        string Title { get; }
        string Description { get; }
        Sprite Icon { get; }

        bool IsStackableItem();
        void GetStackInfo(out int current, out int size);
        bool IsConsumableItem();
        bool CanConsumeItem();
        void OnConsumeClicked();
    }
}