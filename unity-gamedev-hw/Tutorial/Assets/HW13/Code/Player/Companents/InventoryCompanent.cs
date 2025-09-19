using System;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using BehaviorTree.PlayerCoreSubsystem;

namespace BehaviorTree.PlayerCompanents {

    [Serializable]
    public class InventoryCompanent : IPlayerCompanent {
        public event Action<int> CurrentAmountChanged;

        public Transform Root { get; private set; }
        public bool IsActive { get; private set; }

        public bool IsFill {
            get {

                if (CurrentAmount == MaxAmount)
                    return true;
                else
                    return false;
            }
        }

        public int VacantPlace {
            get {
                return (MaxAmount - CurrentAmount);
            }
        }

        public int CurrentAmount {
            get {

                if (_items == null || _items.Count == 0)
                    return 0;

                return _items[0].Amount;
            }
        }

        public int MaxAmount { get; private set; }


        [ShowInInspector]
        private readonly List<InventoryItem> _items = new();

        public void Init(Transform root, PlayerConfig config) {
            Root = root;
            IsActive = true;
            MaxAmount = config.InventoryMaxAmount;
        }

        public void Activate(bool status) {
            IsActive = status;
        }

        public bool TryAddItem(InventoryItem item) {
            if (item == null)
                return false;

            if (TryHandleStackableItem(item) == true)
                return true;

            _items.Add(item);
            return true;
        }

        public bool TryRemoveItem(InventoryItem item) {
            if (item == null)
                return false;

            if (TryHandleStackableItemDecrement(item) == true)
                return true;

            if (TryFindItem(item.ID, out var inventoryItem) == false)
                return false;

            _items.Remove(inventoryItem);
            CurrentAmountChanged?.Invoke(CurrentAmount);

            return true;
        }

        private bool TryHandleStackableItem(InventoryItem item) {

            if (TryFindItem(item.ID, out var inventoryItem) == false) {
                return false;
            }

            inventoryItem.Amount += item.Amount;
            CurrentAmountChanged?.Invoke(CurrentAmount);

            return true;
        }

        private bool TryHandleStackableItemDecrement(InventoryItem item) {

            if (TryFindItem(item.ID, out var inventoryItem) == false)
                return false;

            if (inventoryItem.Amount > item.Amount) {
                inventoryItem.Amount -= item.Amount;
                CurrentAmountChanged?.Invoke(CurrentAmount);

                return true;
            }

            return false;
        }

        public bool TryFindItem(string id, out InventoryItem item) {
            item = _items.FirstOrDefault(i => i.ID == id);
            return item != null;
        }
    }
}

