using System;
using UnityEngine;

namespace InventorySystem.ItemComponents {

    [Serializable]
    public sealed class StackableItemComponent : IItemComponent {
        public event Action<int> OnValueChanged;
        public bool IsFilled => Count == MaxCount;
        public int FreeSeat => MaxCount - Count;

        [field: SerializeField] public int Count { get; private set; }
        [field: SerializeField] public int MaxCount { get; private set; }

        public StackableItemComponent(int count, int maxCount) {
            Count = count;
            MaxCount = maxCount;
        }

        public bool TryAddCount() {

            Count++;

            if (Count > MaxCount) {
                Count = MaxCount;
                return false;
            }

            OnValueChanged?.Invoke(Count);
            return true;
        }

        public bool TryRemoveCount() {
            Count--;

            if (Count > 0) {
                OnValueChanged?.Invoke(Count);
                return true;
            }
                
            return false;
        }

        public IItemComponent Clone() {
            return new StackableItemComponent(Count, MaxCount);
        }
    }
}

