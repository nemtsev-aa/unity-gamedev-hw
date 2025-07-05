using System;
using System.Linq;
using System.Collections.Generic;
using InventorySystem.ItemComponents;

namespace InventorySystem.Core {

    public sealed class Inventory : IInventory {
        public event Action<InventoryItem> ItemAdded;
        public event Action<InventoryItem> ItemRemoved;
        public IReadOnlyList<InventoryItem> Items => _items.AsReadOnly();

        private readonly List<InventoryItem> _items = new();

        public bool TryAddItem(InventoryItem item) {
            if (item == null)
                return false;

            if (TryHandleStackableItem(item) == true)
                return true;

            _items.Add(item);
            ItemAdded?.Invoke(item);
            
            return true;
        }

        public bool TryRemoveItem(InventoryItem item) {
            if (item == null)
                return false;

            if (item.TryGetComponent<StackableItemComponent>(out var component) == true) 
                return TryHandleStackableItemDecrement(item);

            if (TryFindItem(item.ID, out var inventoryItem) == false)
                return false;

            _items.Remove(inventoryItem);
            ItemRemoved?.Invoke(inventoryItem);

            return true;
        }

        public bool TryFindItem(string id, out InventoryItem item) {
            item = _items.FirstOrDefault(i => i.ID == id);
            return item != null;
        }

        private bool TryHandleStackableItem(InventoryItem item) {

            if (item.TryGetComponent<StackableItemComponent>(out var component) == false)
                return false;

            if (TryFindStackableItems(item.ID, out List<StackableItemComponent> stacks) == false)
                return false;

            var stack = GetStackWithFreeSeat(stacks);
            return stack.TryAddCount();
        }

        private bool TryFindStackableItems(string id, out List<StackableItemComponent> stacks) {

            stacks = new();

            for (int i = 0; i < _items.Count; i++) {
                var item = _items[i];

                if (item.ID != id)
                    continue;

                if (item.TryGetComponent<StackableItemComponent>(out var stack) == false)
                    continue;

                stacks.Add(stack);
            }

            return stacks.Count > 0;
        }

        private StackableItemComponent GetStackWithFreeSeat(List<StackableItemComponent> stacks) {
            var targetStack = stacks[0];
            int freeSeat = 0;

            for (int i = 0; i < stacks.Count; i++) {
                var iStack = stacks[i];
                var iFreeSeat = iStack.FreeSeat;

                if (iFreeSeat > freeSeat) {
                    freeSeat = iFreeSeat;
                    targetStack = iStack;
                }
            }

            return targetStack;
        }

        private bool TryHandleStackableItemDecrement(InventoryItem item) {

            item.TryGetComponent<StackableItemComponent>(out var component);

            if (component.TryRemoveCount() == false) {
                _items.Remove(item);
                ItemRemoved?.Invoke(item);
            }

            return true;
        }

        private StackableItemComponent GetStackWithMinCurrentCount(List<StackableItemComponent> stacks) {

            var targetStack = stacks[0];
            int currentCount = stacks[0].MaxCount;

            for (int i = 0; i < stacks.Count; i++) {
                var iStack = stacks[i];
                var iCurrentCount = iStack.Count;

                if (iCurrentCount < currentCount && iCurrentCount > 0) {
                    currentCount = iCurrentCount;
                    targetStack = iStack;
                }
            }

            return targetStack;
        }
    }
}

